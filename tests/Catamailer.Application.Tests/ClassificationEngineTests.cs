// Historique :
// 2026-09-07 : Création des tests pour ClassificationEngine (J1-S2-T1).
// 2026-09-07 : Ajout du test d'extraction de la chaîne d'ascendance (J1-S2-T1).
// 2026-09-07 : Ajout du test de correspondance sur les destinataires (J1-S2-T1).

using System.Collections.Generic;
using Catamailer.Domain;
using Xunit;

namespace Catamailer.Application.Tests
{
    /// <summary>
    /// Classe de test validant le comportement du moteur de classification (Étape 1).
    /// </summary>
    public class ClassificationEngineTests
    {
        [Fact]
        public void Classify_ShouldReturnNull_WhenNoRulesProvided()
        {
            // Arrange
            var engine = new ClassificationEngine();
            var rules = new List<DictionaryRule>();

            // Act
            ClassificationResult? result = engine.Classify("Sujet de test", "expediteur@test.com", new List<string>(), rules);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Classify_ShouldReturnCategoryAndAscendanceChain_WhenKeywordMatches()
        {
            // Arrange
            var engine = new ClassificationEngine();
            
            var rootCategory = new CategoryNode("Projet");
            var childCategory = new CategoryNode("Alpha");
            rootCategory.AddChild(childCategory);

            var rule = new DictionaryRule(childCategory, new[] { "Urgent" });
            var rules = new List<DictionaryRule> { rule };

            // Act
            ClassificationResult? result = engine.Classify("Message Urgent pour le projet", "test@test.com", new List<string>(), rules);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alpha", result.MatchedCategory.Name);
            Assert.Equal(2, result.AppliedCategories.Count);
            Assert.Equal("Projet", result.AppliedCategories[0].Name);
            Assert.Equal("Alpha", result.AppliedCategories[1].Name);
        }

        [Fact]
        public void Classify_ShouldReturnCategory_WhenKeywordMatchesRecipient()
        {
            // Arrange
            var engine = new ClassificationEngine();

            var category = new CategoryNode("Direction");
            var rule = new DictionaryRule(category, new[] { "boss@company.com" });
            var rules = new List<DictionaryRule> { rule };

            var recipients = new List<string> { "employe@company.com", "boss@company.com" };

            // Act
            ClassificationResult? result = engine.Classify("Sujet normal", "expediteur@test.com", recipients, rules);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Direction", result.MatchedCategory.Name);
        }
    }
}