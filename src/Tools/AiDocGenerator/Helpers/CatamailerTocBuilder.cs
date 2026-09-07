// -----------------------------------------------------------------------
// <copyright file="CatamailerTocBuilder.cs" company="Catamailer">
// Copyright (c) Catamailer. All rights reserved.
// </copyright>
// <date>2026-09-07</date>
// <summary>Générateur de table des matières (TOC) pour DocFX, spécifique à Catamailer.</summary>
// -----------------------------------------------------------------------

using System.Text;

namespace AiDocGenerator.Helpers;

/// <summary>
/// Classe responsable de la génération du contenu YAML pour la table des matières principale
/// de la documentation de la solution Catamailer.
/// </summary>
public class CatamailerTocBuilder
{
    /// <summary>
    /// Construit le contenu YAML de la table des matières principale.
    /// </summary>
    /// <returns>Une chaîne de caractères représentant le contenu attendu pour toc.yml.</returns>
    public string Build()
    {
        var sb = new StringBuilder();
        sb.AppendLine("- name: Accueil API");
        sb.AppendLine("  href: index.md");
        sb.AppendLine();
        sb.AppendLine("- href: Domain/toc.yml");
        sb.AppendLine();
        sb.AppendLine("- href: Application/toc.yml");
        sb.AppendLine();
        sb.AppendLine("- href: Infrastructure/toc.yml");
        sb.AppendLine();
        sb.AppendLine("- href: UI/toc.yml");
        sb.AppendLine();
        sb.AppendLine("- href: Migrator/toc.yml");
        sb.AppendLine();
        
        return sb.ToString();
    }
}