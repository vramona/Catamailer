using System.IO;
using System.Text.RegularExpressions;

namespace AiDocGenerator.Analyzers;

public class RazorComponentInfo
{
    public string? Route { get; set; }
    public string RelativePath { get; set; } = string.Empty;
}

public static class RazorAnalyzer
{
    public static RazorComponentInfo ExtractComponentInfo(string fileContent, string absoluteFilePath, string solutionDir)
    {
        var relativePath = string.IsNullOrEmpty(solutionDir)
            ? absoluteFilePath
            : Path.GetRelativePath(solutionDir, absoluteFilePath);

        var match = Regex.Match(fileContent, @"^@page\s+""([^""]+)""", RegexOptions.Multiline);

        return new RazorComponentInfo
        {
            Route = match.Success ? match.Groups[1].Value : null,
            RelativePath = relativePath
        };
    }
}