// Historique :
// 2026-09-08 : Création des tests de validation pour QuickCategorizeViewModel (J3-S2-T1).
// 2026-09-08 : Mise à jour du mock et appel à InitializeAsync pour corriger le passage au vert (J3-S2-T1).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catamailer.Application.ViewModels;
using Catamailer.Domain;
using Moq;
using Xunit;

namespace Catamailer.Application.Tests.ViewModels;

/// <summary>
/// Tests unitaires validant la logique de recherche et de tri de la modale Quick Categorize.
/// </summary>
public class QuickCategorizeViewModelTests
{
    private readonly Mock<ICategoryRepository> _mockCategoryRepo;
    private readonly List<CategoryNode> _categories;

    public QuickCategorizeViewModelTests()
    {
        _mockCategoryRepo = new Mock<ICategoryRepository>();
        
        _categories = new List<CategoryNode>
        {
            new CategoryNode("Projet Alpha", "#FF0000"),
            new CategoryNode("Projet Beta", "#00FF00"),
            new CategoryNode("Urgent", "#0000FF"),
            new CategoryNode("Alpha Team", "#FFFF00")
        };

        // Configuration du Mock pour retourner la liste de test
        _mockCategoryRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(_categories);
    }

    [Fact]
    public async Task UpdateSearchAsync_ShouldFilterCategories_IgnoringCase()
    {
        // Arrange
        var viewModel = new QuickCategorizeViewModel(_mockCategoryRepo.Object);
        await viewModel.InitializeAsync(); // Indispensable pour charger _allCategories

        // Act
        await viewModel.UpdateSearchAsync("alpha");

        // Assert
        Assert.NotNull(viewModel.FilteredCategories);
        Assert.Equal(2, viewModel.FilteredCategories.Count());
        Assert.Contains(viewModel.FilteredCategories, c => c.Name == "Projet Alpha");
        Assert.Contains(viewModel.FilteredCategories, c => c.Name == "Alpha Team");
    }

    [Fact]
    public async Task UpdateSearchAsync_ShouldReturnEmpty_WhenNoMatchFound()
    {
        // Arrange
        var viewModel = new QuickCategorizeViewModel(_mockCategoryRepo.Object);
        await viewModel.InitializeAsync();

        // Act
        await viewModel.UpdateSearchAsync("Zeta");

        // Assert
        Assert.NotNull(viewModel.FilteredCategories);
        Assert.Empty(viewModel.FilteredCategories);
    }
}