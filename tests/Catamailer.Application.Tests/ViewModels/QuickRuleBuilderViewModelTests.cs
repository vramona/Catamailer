// Historique :
// 2026-09-08 : Création des tests initiaux pour QuickRuleBuilderViewModel (J3-S2-T2).
// 2026-09-08 : Correction des signatures suite à la récupération des entités de domaine (J3-S2-T2).
// 2026-09-09 : Adaptation aux refontes de MailMetadata et DictionaryRule (J3-S2-T2-ST1).
// 2026-09-09 : Ajout des tests pour l'IHM à 2 colonnes (Options sélectionnables et catégories déclenchées) (J3-S2-T2-ST2).
// 2026-09-09 : Refactoring xUnit1031, passage de BuildRule_ShouldOnlyIncludeSelectedOptions en async (J3-S3-T1 - Phase Orange).

using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly Mock<IRuleRepository> _ruleRepositoryMock; // Nouveau dépôt pour les règles
    private readonly ClassificationEngine _classificationEngine;

    public QuickRuleBuilderViewModelTests()
    {
        _selectionProviderMock = new Mock<ISelectionProvider>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _ruleRepositoryMock = new Mock<IRuleRepository>();
        _classificationEngine = new ClassificationEngine();
    }

    private QuickRuleBuilderViewModel CreateViewModel()
    {
        return new QuickRuleBuilderViewModel(
            _selectionProviderMock.Object, 
            _categoryRepositoryMock.Object,
            _ruleRepositoryMock.Object,
            _classificationEngine);
    }

    [Fact]
    public async Task InitializeAsync_ShouldPopulateSelectableOptions_FromSelectedMail()
    {
        // Arrange
        var fakeMetadata = new MailMetadata(
            "12345",
            "Test Subject",
            "sender@example.com",
            "boss@example.com", // OnBehalfOf
            new List<string> { "internal@example.com" }, // Doit être ignoré pour la création
            new List<string> { "client1@example.com", "client2@example.com" }); // Destinataires externes

        _selectionProviderMock.Setup(sp => sp.GetSelectedMail()).Returns(fakeMetadata);
        _ruleRepositoryMock.Setup(rr => rr.GetAllDictionaryRulesAsync()).ReturnsAsync(new List<DictionaryRule>());

        var viewModel = CreateViewModel();

        // Act
        await viewModel.InitializeAsync();

        // Assert
        // Vérification de la structure d'options pour l'IHM (cases à cocher)
        Assert.NotNull(viewModel.SubjectOption);
        Assert.Equal("Test Subject", viewModel.SubjectOption.Value);
        Assert.False(viewModel.SubjectOption.IsSelected); // Non coché par défaut

        Assert.Equal(2, viewModel.SenderOptions.Count);
        Assert.Contains(viewModel.SenderOptions, o => o.Value == "sender@example.com");
        Assert.Contains(viewModel.SenderOptions, o => o.Value == "boss@example.com");

        Assert.Equal(2, viewModel.RecipientOptions.Count);
        Assert.Contains(viewModel.RecipientOptions, o => o.Value == "client1@example.com");
        Assert.DoesNotContain(viewModel.RecipientOptions, o => o.Value == "internal@example.com"); // Internes ignorés
    }

    [Fact]
    public async Task InitializeAsync_ShouldIdentifyTriggeredCategories_WhenRulesMatch()
    {
        // Arrange
        var fakeMetadata = new MailMetadata(
            "12345", "Urgent Subject", "sender@example.com", null, new List<string>(), new List<string>());
        
        _selectionProviderMock.Setup(sp => sp.GetSelectedMail()).Returns(fakeMetadata);

        var urgentCategory = new CategoryNode("Urgent");
        var rule = new DictionaryRule(urgentCategory, subjectKeywords: new[] { "Urgent" });
        
        _ruleRepositoryMock.Setup(rr => rr.GetAllDictionaryRulesAsync()).ReturnsAsync(new List<DictionaryRule> { rule });

        var viewModel = CreateViewModel();

        // Act
        await viewModel.InitializeAsync();

        // Assert
        Assert.Single(viewModel.TriggeredCategories);
        Assert.Equal("Urgent", viewModel.TriggeredCategories.First().Name);
    }

    [Fact]
    public async Task BuildRule_ShouldOnlyIncludeSelectedOptions()
    {
        // Arrange
        var targetCategory = new CategoryNode("Target");
        
        var fakeMetadata = new MailMetadata(
            "12345", "Invoice", "billing@corp.com", null, new List<string>(), new List<string> { "client@corp.com" });
        _selectionProviderMock.Setup(sp => sp.GetSelectedMail()).Returns(fakeMetadata);
        _ruleRepositoryMock.Setup(rr => rr.GetAllDictionaryRulesAsync()).ReturnsAsync(new List<DictionaryRule>());

        var viewModel = CreateViewModel();
        await viewModel.InitializeAsync(); // Remplissage initial sans appel bloquant .Wait()

        viewModel.SelectCategory(targetCategory);

        // Simulation des actions de l'utilisateur sur les cases à cocher
        viewModel.SubjectOption!.IsSelected = true;
        viewModel.SenderOptions.First(o => o.Value == "billing@corp.com").IsSelected = false; // Laissé décoché
        viewModel.RecipientOptions.First(o => o.Value == "client@corp.com").IsSelected = true;

        // Act
        var resultRule = viewModel.BuildRule();

        // Assert
        Assert.NotNull(resultRule);
        Assert.Equal(targetCategory, resultRule.TargetCategory);
        
        // Seules les options cochées doivent être présentes dans la règle générée
        Assert.Single(resultRule.SubjectKeywords);
        Assert.Contains("Invoice", resultRule.SubjectKeywords);
        
        Assert.Empty(resultRule.SenderKeywords); // Décoché
        
        Assert.Single(resultRule.RecipientKeywords);
        Assert.Contains("client@corp.com", resultRule.RecipientKeywords);
    }
}