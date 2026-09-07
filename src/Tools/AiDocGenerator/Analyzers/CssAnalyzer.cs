using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AiDocGenerator.Analyzers;

public class CssComponentInfo
{
    public string RelativePath { get; set; } = string.Empty;
    public IEnumerable<string> Classes { get; set; } = Enumerable.Empty<string>();
}

public static class CssAnalyzer
{
    public static CssComponentInfo ExtractCssInfo(string fileContent, string absoluteFilePath, string solutionDir)
    {
        var relativePath = string.IsNullOrEmpty(solutionDir)
            ? absoluteFilePath
            : Path.GetRelativePath(solutionDir, absoluteFilePath);

        var matches = Regex.Matches(fileContent, @"\.([a-zA-Z0-9_-]+)");
        var classes = matches.Select(m => m.Groups[1].Value).Distinct().ToList();

        return new CssComponentInfo
        {
            RelativePath = relativePath,
            Classes = classes
        };
    }
}