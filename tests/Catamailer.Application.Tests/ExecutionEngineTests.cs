// Historique :
// 2026-09-07 : Création des tests pour ExecutionEngine (J1-S2-T2).
// 2026-09-07 : Ajout des tests pour l'évaluation d'un critère simple sur l'expéditeur (J1-S2-T2).
// 2026-09-07 : Ajout des tests pour l'opérateur Contains et la logique Or (J1-S2-T2).

using Catamailer.Domain;
using Xunit;

namespace Catamailer.Application.Tests
{
    /// <summary>
    /// Classe de test validant le comportement du moteur d'exécution (Étape 2).
    /// </summary>
    public class ExecutionEngineTests
    {
        [Fact]
        public void Evaluate_ShouldReturnFalse_WhenNodeIsEmpty()
        {
            // Arrange
            var engine = new ExecutionEngine();
            var node = new RuleNode(LogicalOperator.And);

            // Act
            var result = engine.Evaluate("Sujet de test", "expediteur@test.com", node);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Evaluate_ShouldReturnTrue_WhenSenderMatchesEqualsCriterion()
        {
            // Arrange
            var engine = new ExecutionEngine();
            var node = new RuleNode(LogicalOperator.And);
            node.AddCriterion(new RuleCriterion(MailField.Sender, MatchOperator.Equals, "boss@company.com"));

            // Act
            var result = engine.Evaluate("Sujet", "boss@company.com", node);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Evaluate_ShouldReturnFalse_WhenSenderDoesNotMatchEqualsCriterion()
        {
            // Arrange
            var engine = new ExecutionEngine();
            var node = new RuleNode(LogicalOperator.And);
            node.AddCriterion(new RuleCriterion(MailField.Sender, MatchOperator.Equals, "boss@company.com"));

            // Act
            var result = engine.Evaluate("Sujet", "employe@company.com", node);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Evaluate_ShouldReturnTrue_WhenSubjectMatchesContainsCriterion()
        {
            // Arrange
            var engine = new ExecutionEngine();
            var node = new RuleNode(LogicalOperator.And);
            node.AddCriterion(new RuleCriterion(MailField.Subject, MatchOperator.Contains, "Urgent"));

            // Act
            var result = engine.Evaluate("Ceci est un message URGENT !", "expediteur@test.com", node);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Evaluate_ShouldReturnTrue_WhenOrNodeHasOneValidCriterion()
        {
            // Arrange
            var engine = new ExecutionEngine();
            var node = new RuleNode(LogicalOperator.Or);
            node.AddCriterion(new RuleCriterion(MailField.Sender, MatchOperator.Equals, "inconnu@test.com"));
            node.AddCriterion(new RuleCriterion(MailField.Subject, MatchOperator.Contains, "Facture"));

            // Act
            var result = engine.Evaluate("Votre facture mensuelle", "compta@entreprise.com", node);

            // Assert
            Assert.True(result);
        }
    }
}