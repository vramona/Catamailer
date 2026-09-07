using Xunit;
using AiDocGenerator.Helpers;

namespace Tools.AiDocGenerator.Tests;

public class CatamailerTocBuilderTests
{
    /// <summary>
    /// Vérifie que le TOC généré est spécifiquement adapté à l'architecture de Catamailer
    /// et ne contient plus aucune référence à l'ancien projet Sudoku.
    /// </summary>
    [Fact]
    public void Build_ShouldReturnCatamailerSpecificToc()
    {
        // Arrange
        var builder = new CatamailerTocBuilder();

        // Act
        string toc = builder.Build();

        // Assert
        Assert.Contains("- href: Domain/toc.yml", toc);
        Assert.Contains("- href: Application/toc.yml", toc);
        Assert.Contains("- href: Infrastructure/toc.yml", toc);
        Assert.Contains("- href: UI/toc.yml", toc);
        Assert.Contains("- href: Migrator/toc.yml", toc);
        Assert.DoesNotContain("Sudoku", toc);
        Assert.DoesNotContain("ComputerVision", toc);
    }
}