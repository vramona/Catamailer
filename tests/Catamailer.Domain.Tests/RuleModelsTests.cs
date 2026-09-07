// Historique :
// 2026-09-07 : Création des tests pour la modélisation des règles (J1-S1-T2).

using System.Linq;
using Xunit;

namespace Catamailer.Domain.Tests
{
    /// <summary>
    /// Classe de test validant la modélisation des entités de règles (DictionaryRule, RuleNode, RuleCriterion, RuleAction).
    /// </summary>
    public class RuleModelsTests
    {
        [Fact]
        public void DictionaryRule_Creation_ShouldSetProperties()
        {
            // Arrange
            var targetCategory = new CategoryNode("Factures");
            var keywords = new[] { "invoice", "facture", "reçu" };

            // Act
            var rule = new DictionaryRule(targetCategory, keywords);

            // Assert
            Assert.Equal(targetCategory, rule.TargetCategory);
            Assert.Equal(3, rule.Keywords.Count);
            Assert.Contains("facture", rule.Keywords);
        }

        [Fact]
        public void RuleAction_Creation_ShouldSetActionTypeAndParameter()
        {
            // Arrange & Act
            var action = new RuleAction(ActionType.MoveToFolder, "Comptabilité");

            // Assert
            Assert.Equal(ActionType.MoveToFolder, action.Type);
            Assert.Equal("Comptabilité", action.Parameter);
        }

        [Fact]
        public void RuleCriterion_Creation_ShouldSetConditionFields()
        {
            // Arrange & Act
            var criterion = new RuleCriterion(MailField.Subject, MatchOperator.Contains, "Urgent");

            // Assert
            Assert.Equal(MailField.Subject, criterion.Field);
            Assert.Equal(MatchOperator.Contains, criterion.Operator);
            Assert.Equal("Urgent", criterion.Value);
        }

        [Fact]
        public void RuleNode_ShouldActAsComposite_HoldingCriteriaAndChildNodes()
        {
            // Arrange
            var rootNode = new RuleNode(LogicalOperator.Or);
            var childNode = new RuleNode(LogicalOperator.And);
            
            var criterion1 = new RuleCriterion(MailField.Sender, MatchOperator.Equals, "boss@company.com");
            var criterion2 = new RuleCriterion(MailField.Subject, MatchOperator.Contains, "Validation");

            // Act
            childNode.AddCriterion(criterion1);
            childNode.AddCriterion(criterion2);
            rootNode.AddChildNode(childNode);

            // Assert
            Assert.Equal(LogicalOperator.Or, rootNode.Operator);
            Assert.Single(rootNode.ChildNodes);
            Assert.Empty(rootNode.Criteria);
            
            var retrievedChild = rootNode.ChildNodes.First();
            Assert.Equal(LogicalOperator.And, retrievedChild.Operator);
            Assert.Equal(2, retrievedChild.Criteria.Count);
        }
    }
}