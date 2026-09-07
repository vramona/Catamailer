using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AiDocGenerator.Analyzers
{
    /// <summary>
    /// Représente un paramètre extrait d'un script PowerShell.
    /// </summary>
    public class ScriptParameter
    {
        /// <summary>
        /// Obtient ou définit le nom du paramètre (sans le $).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le type du paramètre (ex: string, int).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Indique si le paramètre est obligatoire (Mandatory=$true).
        /// </summary>
        public bool IsMandatory { get; set; }
    }

    /// <summary>
    /// Contient les métadonnées et dépendances extraites d'un fichier script PowerShell (.ps1).
    /// </summary>
    public class PowerShellScriptInfo
    {
        /// <summary>
        /// Obtient ou définit le nom du fichier script.
        /// </summary>
        public string ScriptName { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit le chemin relatif du script par rapport à la racine de la solution.
        /// </summary>
        public string RelativePath { get; set; } = string.Empty;

        /// <summary>
        /// Obtient la liste des paramètres déclarés dans le bloc param() du script.
        /// </summary>
        public List<ScriptParameter> Parameters { get; set; } = new();

        /// <summary>
        /// Obtient la liste des noms des autres scripts PowerShell invoqués par ce script.
        /// </summary>
        public List<string> CalledScripts { get; set; } = new();
    }

    /// <summary>
    /// Analyseur statique chargé de parser le contenu des fichiers PowerShell (.ps1)
    /// pour extraire l'arbre des invocations et les paramètres via des expressions régulières.
    /// </summary>
    public static class PowerShellAnalyzer
    {
        /// <summary>
        /// Extrait les informations d'un script PowerShell à partir de son contenu texte.
        /// </summary>
        /// <param name="fileContent">Le contenu brut du fichier .ps1.</param>
        /// <param name="absoluteFilePath">Le chemin absolu du fichier analysé.</param>
        /// <param name="solutionDir">Le répertoire racine de la solution pour calculer le chemin relatif.</param>
        /// <returns>Un objet <see cref="PowerShellScriptInfo"/> contenant les données extraites.</returns>
        public static PowerShellScriptInfo ExtractScriptInfo(string fileContent, string absoluteFilePath, string solutionDir)
        {
            var info = new PowerShellScriptInfo
            {
                ScriptName = Path.GetFileName(absoluteFilePath),
                RelativePath = absoluteFilePath.Replace(solutionDir, "").TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
            };

            // Étape 1 : Extraction du bloc param() avec un groupe de balancement (Balancing Group) pour gérer les parenthèses imbriquées
            var paramBlockMatch = Regex.Match(fileContent, @"param\s*\((?<block>(?:[^()]+|\((?<DEPTH>)|\)(?<-DEPTH>))*(?(DEPTH)(?!)))\)", RegexOptions.IgnoreCase);
            if (paramBlockMatch.Success)
            {
                string paramBlock = paramBlockMatch.Groups["block"].Value;

                // Regex ciblant chaque paramètre individuel dans le bloc capturé
                var paramRegex = new Regex(@"(?:\[Parameter\([^)]*Mandatory\s*=\s*\$(?<mandatory>true|false)[^)]*\)\]\s*)?(?:\[(?<type>[a-zA-Z0-9_\.\[\]]+)\]\s*)?\$(?<name>[a-zA-Z0-9_]+)", RegexOptions.IgnoreCase);
                var paramMatches = paramRegex.Matches(paramBlock);

                foreach (Match m in paramMatches)
                {
                    info.Parameters.Add(new ScriptParameter
                    {
                        Name = m.Groups["name"].Value,
                        Type = m.Groups["type"].Success ? m.Groups["type"].Value : "string", // string par défaut si non typé
                        IsMandatory = m.Groups["mandatory"].Success && m.Groups["mandatory"].Value.Equals("true", StringComparison.OrdinalIgnoreCase)
                    });
                }
            }

            // Étape 2 : Extraction des dépendances (Appels à d'autres scripts .ps1)
            var callRegex = new Regex(@"[\\/](?<script>[a-zA-Z0-9_-]+\.ps1)[\""']?", RegexOptions.IgnoreCase);
            var callMatches = callRegex.Matches(fileContent);

            foreach (Match m in callMatches)
            {
                string calledScript = m.Groups["script"].Value;

                // Exclut les auto-appels et les doublons
                if (!string.Equals(calledScript, info.ScriptName, StringComparison.OrdinalIgnoreCase) &&
                    !info.CalledScripts.Contains(calledScript))
                {
                    info.CalledScripts.Add(calledScript);
                }
            }

            return info;
        }
    }
}