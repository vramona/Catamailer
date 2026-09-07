using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AiDocGenerator.Analyzers;

public class TestDetail
{
    public string Name { get; set; } = string.Empty;
    public string Outcome { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
}

public class TestRunSummary
{
    public int Total { get; set; }
    public int Passed { get; set; }
    public int Failed { get; set; }
    public string? ErrorMessage { get; set; }
    public List<TestDetail> AllTests { get; set; } = new();
}

public static class TestResultsAnalyzer
{
    public static TestRunSummary ParseTrx(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new TestRunSummary { ErrorMessage = "Fichier de résultats introuvable." };
        }

        try
        {
            var doc = XDocument.Load(filePath);
            var ns = doc.Root?.Name.Namespace ?? XNamespace.None;

            var counters = doc.Descendants(ns + "Counters").FirstOrDefault();
            int total = 0, passed = 0, failed = 0;

            if (counters != null)
            {
                int.TryParse(counters.Attribute("total")?.Value, out total);
                int.TryParse(counters.Attribute("passed")?.Value, out passed);
                int.TryParse(counters.Attribute("failed")?.Value, out failed);
            }

            var allTests = doc.Descendants(ns + "UnitTestResult")
                .Select(x => new TestDetail
                {
                    Name = x.Attribute("testName")?.Value ?? "Inconnu",
                    Outcome = x.Attribute("outcome")?.Value ?? "Inconnu",
                    Duration = x.Attribute("duration")?.Value ?? "00:00:00"
                })
                .ToList();

            return new TestRunSummary
            {
                Total = total,
                Passed = passed,
                Failed = failed,
                AllTests = allTests
            };
        }
        catch (Exception ex)
        {
            return new TestRunSummary { ErrorMessage = $"Erreur de parsing XML : {ex.Message}" };
        }
    }
}