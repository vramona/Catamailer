// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Catamailer">
// Copyright (c) Catamailer. All rights reserved.
// </copyright>
// <date>2026-09-07</date>
// <summary>Point d'entrée de l'outil de génération de documentation AI.</summary>
// -----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using AiDocGenerator.Analyzers;
using AiDocGenerator.Helpers;

MSBuildLocator.RegisterDefaults();

if (args.Length < 2)
{
    Console.WriteLine("Usage: AiDocGenerator <Chemin_Vers_Solution.sln> <Fichier_Sortie.md>");
    return;
}

string solutionPath = args[0];
string solutionDir = Path.GetDirectoryName(solutionPath) ?? string.Empty;
string outputPath = args[1];

Console.WriteLine($"Chargement de la solution : {solutionPath}...");

MSBuildWorkspace? workspace = null;

// Instanciation de l'encodeur UTF-8 avec BOM pour forcer la reconnaissance par Visual Studio et DocFX
var utf8WithBom = new UTF8Encoding(true);

// -------------------------------------------------------------------------
// Initialisation propre de la TOC globale Catamailer
// -------------------------------------------------------------------------
var globalApiDir = Path.Combine(solutionDir, "Doc", "api");
Directory.CreateDirectory(globalApiDir);
var globalTocPath = Path.Combine(globalApiDir, "toc.yml");

// Utilisation du builder testé unitairement pour la Clean Architecture
var initialTocContent = new CatamailerTocBuilder().Build();
File.WriteAllText(globalTocPath, initialTocContent, utf8WithBom);

try
{
    workspace = MSBuildWorkspace.Create();
    var solution = await workspace.OpenSolutionAsync(solutionPath);

    var markdownBuilder = new StringBuilder();
    markdownBuilder.AppendLine("# Contexte d'Architecture IA et Arbre des Invocations");
    markdownBuilder.AppendLine($"Généré le : {DateTime.Now:yyyy-MM-dd HH:mm}");
    markdownBuilder.AppendLine();

    foreach (var project in solution.Projects)
    {
        markdownBuilder.AppendLine($"## Projet : {project.Name}");

        var compilation = await project.GetCompilationAsync();
        if (compilation == null) continue;

        // Analyse C#
        var csharpDoc = await CSharpAnalyzer.AnalyzeProjectAsync(project, solutionDir);
        markdownBuilder.Append(csharpDoc);

        // Analyse Razor & CSS
        var projectDir = Path.GetDirectoryName(project.FilePath);
        if (!string.IsNullOrEmpty(projectDir) && Directory.Exists(projectDir))
        {
            var razorFiles = Directory.EnumerateFiles(projectDir, "*.razor", SearchOption.AllDirectories)
                .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") &&
                            !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")).ToList();

            var cssFiles = Directory.EnumerateFiles(projectDir, "*.css", SearchOption.AllDirectories)
                .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") &&
                            !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}")).ToList();

            if (razorFiles.Any())
            {
                markdownBuilder.AppendLine();
                markdownBuilder.AppendLine("### Composants Razor");
                foreach (var razorFile in razorFiles)
                {
                    var content = File.ReadAllText(razorFile);
                    var info = RazorAnalyzer.ExtractComponentInfo(content, razorFile, solutionDir);
                    var routeDisplay = info.Route != null ? $" (Route: `{info.Route}`)" : "";
                    markdownBuilder.AppendLine($"- **{Path.GetFileNameWithoutExtension(razorFile)}**{routeDisplay} : `{info.RelativePath}`");
                }
                markdownBuilder.AppendLine();
            }
            
            // NOTE : Le bloc spécifique à Sudoku.UI.Shared a été supprimé lors du refactoring pour Catamailer.
        }
    }

    // Analyse des scripts PowerShell
    var psFiles = Directory.EnumerateFiles(solutionDir, "*.ps1", SearchOption.AllDirectories)
        .Where(f => !f.Contains($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") &&
                    !f.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}") &&
                    !f.Contains($"{Path.DirectorySeparatorChar}.git{Path.DirectorySeparatorChar}")).ToList();

    if (psFiles.Any())
    {
        var scriptsDir = Path.Combine(solutionDir, "Doc", "api", "Scripts");
        Directory.CreateDirectory(scriptsDir);

        var scriptsMd = new StringBuilder();
        scriptsMd.AppendLine("# Documentation des Scripts PowerShell");
        scriptsMd.AppendLine();

        markdownBuilder.AppendLine("### Scripts PowerShell et Outils d'Automatisation");

        foreach (var ps in psFiles)
        {
            var content = File.ReadAllText(ps);
            var info = PowerShellAnalyzer.ExtractScriptInfo(content, ps, solutionDir);

            // Construction pour DocFX
            scriptsMd.AppendLine($"## Script : {info.ScriptName}");
            scriptsMd.AppendLine($"**Chemin relatif** : `{info.RelativePath}`");
            scriptsMd.AppendLine();

            // Construction pour Ai_Architecture.md
            markdownBuilder.AppendLine($"- **{info.ScriptName}** : `{info.RelativePath}`");

            if (info.Parameters.Any())
            {
                scriptsMd.AppendLine("### Paramètres");
                foreach (var p in info.Parameters)
                {
                    string mandatory = p.IsMandatory ? " *(Obligatoire)*" : "";
                    scriptsMd.AppendLine($"- `[{p.Type}] ${p.Name}`{mandatory}");
                    markdownBuilder.AppendLine($"  - Paramètre : `[{p.Type}] ${p.Name}`{mandatory}");
                }
                scriptsMd.AppendLine();
            }

            if (info.CalledScripts.Any())
            {
                scriptsMd.AppendLine("### Dépendances (Invocations)");
                foreach (var cs in info.CalledScripts)
                {
                    scriptsMd.AppendLine($"- *Appelle* ➡️ `{cs}`");
                    markdownBuilder.AppendLine($"  - *Appelle* ➡️ `{cs}`");
                }
                scriptsMd.AppendLine();
            }

            scriptsMd.AppendLine("---");
        }

        markdownBuilder.AppendLine();

        // Écriture DocFX
        File.WriteAllText(Path.Combine(scriptsDir, "Scripts.md"), scriptsMd.ToString(), utf8WithBom);

        var psToc = "- name: Scripts PowerShell\n  href: Scripts.md\n";
        File.WriteAllText(Path.Combine(scriptsDir, "toc.yml"), psToc, utf8WithBom);

        // Injection dans le toc.yml global DocFX via TocModifier
        TocModifier.InjectEntryIfNeeded(globalTocPath, "Scripts", "Scripts/toc.yml");
    }

    // Analyse des résultats de tests
    var trxPath = Path.Combine(solutionDir, "logs", "TestResults.trx");
    var testSummary = TestResultsAnalyzer.ParseTrx(trxPath);
    var testsReportDir = Path.Combine(solutionDir, "Doc", "api", "TestsReport");
    Directory.CreateDirectory(testsReportDir);

    var reportMd = new StringBuilder();
    reportMd.AppendLine("# Rapport des Tests Unitaires");
    reportMd.AppendLine();

    if (!string.IsNullOrEmpty(testSummary.ErrorMessage))
    {
        reportMd.AppendLine($"**Avertissement :** {testSummary.ErrorMessage}");
    }
    else
    {
        reportMd.AppendLine($"- **Total des tests exécutés** : {testSummary.Total}");
        reportMd.AppendLine($"- **Tests réussis** : {testSummary.Passed} 🟢");
        reportMd.AppendLine($"- **Tests en échec** : {testSummary.Failed} 🔴");
        reportMd.AppendLine();

        reportMd.AppendLine("### Détail de l'exécution");
        reportMd.AppendLine();
        reportMd.AppendLine("| Résultat | Nom du Test | Durée |");
        reportMd.AppendLine("| :---: | :--- | :--- |");

        foreach (var test in testSummary.AllTests)
        {
            string icon = test.Outcome == "Passed" ? "🟢" : (test.Outcome == "Failed" ? "🔴" : "⚪");
            reportMd.AppendLine($"| {icon} | `{test.Name}` | {test.Duration} |");
        }
    }

    File.WriteAllText(Path.Combine(testsReportDir, "TestResults.md"), reportMd.ToString(), utf8WithBom);

    var testsToc = "- name: Synthèse des Résultats\n  href: TestResults.md\n";
    File.WriteAllText(Path.Combine(testsReportDir, "toc.yml"), testsToc, utf8WithBom);

    // Injection dans le toc.yml global DocFX via TocModifier
    TocModifier.InjectEntryIfNeeded(globalTocPath, "Rapport de Tests", "TestsReport/toc.yml");

    var outputDir = Path.GetDirectoryName(outputPath);
    if (!string.IsNullOrWhiteSpace(outputDir) && !Directory.Exists(outputDir))
    {
        Directory.CreateDirectory(outputDir);
    }

    File.WriteAllText(outputPath, markdownBuilder.ToString(), utf8WithBom);
    Console.WriteLine($"Génération terminée avec succès : {outputPath}");
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Erreur critique lors de l'analyse : {ex.Message}");
    Console.WriteLine(ex.StackTrace);
    Console.ResetColor();
}
finally
{
    workspace?.Dispose();
    Console.WriteLine("Nettoyage de l'espace de travail MSBuild terminé.");
}