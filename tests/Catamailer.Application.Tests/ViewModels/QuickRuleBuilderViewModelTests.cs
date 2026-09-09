// Historique :
// 2026-09-08 : Création des tests initiaux pour QuickRuleBuilderViewModel (J3-S2-T2).
// 2026-09-08 : Correction des signatures suite à la récupération des entités de domaine (J3-S2-T2).
// 2026-09-09 : Adaptation aux refontes de MailMetadata et DictionaryRule (J3-S2-T2-ST1).

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catamailer.Application.ViewModels;
using Catamailer.Domain;
using Moq;
using Xunit;

namespace Catamailer.Application.Tests.ViewModels;

public class QuickRuleBuilderViewModelTests
{
    private readonly Mock<ISelectionProvider> _selectionProviderMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

    public QuickRuleBuilderViewModelTests()
    {
        _selectionProviderMock = new Mock<ISelectionProvider>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
    }

    [Fact]
    public async Task InitializeAsync_ShouldPopulateFields_FromSelectedMail()
    {
        // Arrange
        var fakeMetadata = new MailMetadata(
            "12345",
            "Test Subject",
            "sender@example.com",
            null,
            new List<string>(),
            new List<string> { "recipient@example.com" });

        _selectionProviderMock.Setup(sp => sp.GetSelectedMail())
            .Returns(fakeMetadata);

        var viewModel = new QuickRuleBuilderViewModel(_selectionProviderMock.Object, _categoryRepositoryMock.Object);

        // Act
        await viewModel.InitializeAsync();

        // Assert
        Assert.Equal("Test Subject", viewModel.SubjectKeyword);
        Assert.Equal("sender@example.com", viewModel.SenderKeyword);
    }

    [Fact]
    public async Task InitializeAsync_ShouldLeaveFieldsEmpty_WhenNoMailSelected()
    {
        // Arrange
        _selectionProviderMock.Setup(sp => sp.GetSelectedMail())
            .Returns((MailMetadata?)null);

        var viewModel = new QuickRuleBuilderViewModel(_selectionProviderMock.Object, _categoryRepositoryMock.Object);

        // Act
        await viewModel.InitializeAsync();

        // Assert
        Assert.Equal(string.Empty, viewModel.SubjectKeyword);
        Assert.Equal(string.Empty, viewModel.SenderKeyword);
    }

    [Fact]
    public void BuildRule_ShouldReturnPopulatedDictionaryRule()
    {
        // Arrange
        var targetCategory = new CategoryNode("Target", "#000000");
        var viewModel = new QuickRuleBuilderViewModel(_selectionProviderMock.Object, _categoryRepositoryMock.Object);
        
        viewModel.SubjectKeyword = "Invoice";
        viewModel.SenderKeyword = "billing@corp.com";
        viewModel.SelectCategory(targetCategory);

        // Act
        var resultRule = viewModel.BuildRule();

        // Assert
        Assert.NotNull(resultRule);
        Assert.Equal(targetCategory, resultRule.TargetCategory);
        Assert.Contains("Invoice", resultRule.SubjectKeywords);
        Assert.Contains("billing@corp.com", resultRule.SenderKeywords);
    }
}