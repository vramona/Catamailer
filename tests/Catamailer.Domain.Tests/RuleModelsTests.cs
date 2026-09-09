// Historique :
// 2026-09-07 : Création des tests pour la modélisation des règles (J1-S1-T2).
// 2026-09-09 : Adaptation à la refonte de DictionaryRule (J3-S2-T2-ST1).

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
            var subjectKeywords = new[] { "invoice", "facture" };
            var senderKeywords = new[] { "billing@corp.com" };

            // Act
            var rule = new DictionaryRule(targetCategory, subjectKeywords: subjectKeywords, senderKeywords: senderKeywords);

            // Assert
            Assert.Equal(targetCategory, rule.TargetCategory);
            Assert.Equal(2, rule.SubjectKeywords.Count);
            Assert.Single(rule.SenderKeywords);
            Assert.Empty(rule.RecipientKeywords);
            Assert.Contains("facture", rule.SubjectKeywords);
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