// Historique :
// 2026-09-07 : Création des tests pour la modélisation des règles (J1-S1-T2).
// 2026-09-09 : Adaptation à la refonte de DictionaryRule (J3-S2-T2-ST1).
// 2026-09-11 : Ajout des tests pour la mutation de RuleNode (J3-S3-T3-ST2).
// 2026-09-11 : Ajout des tests pour ExecutionRule et MailField.Category (J3-S3-T3-ST2).
// 2026-09-11 : Ajout des tests de validation croisée (Guard clauses) pour RuleCriterion (J3-S3-T3-ST2 - Phase Rouge).
// 2026-09-23 : Ajout des tests pour les nouvelles ActionType du Jalon 5 (J5-S1-T1 - Phase Rouge).
// 2026-09-24 : Remplacement de l'action unique par une collection d'actions dans ExecutionRule (J5-S3-T1 - Phase Rouge).

using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Catamailer.Domain.Tests
{
    /// <summary>
    /// Classe de test validant la modélisation des entités de règles (DictionaryRule, RuleNode, RuleCriterion, RuleAction, ExecutionRule).
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
        public void RuleAction_SupportsNewActionTypes_FromJalon5()
        {
            // Arrange & Act
            // Ces instanciations doivent provoquer une erreur de compilation (Phase Rouge) car les membres n'existent pas encore dans l'enum ActionType.
            var forwardAction = new RuleAction(ActionType.Forward, "boss@company.com");
            var importanceAction = new RuleAction(ActionType.SetImportance, "High");
            var reminderAction = new RuleAction(ActionType.AddReminder, "2026-10-01T09:00:00");
            var flagTodayAction = new RuleAction(ActionType.FlagToday, string.Empty);
            var signatureAction = new RuleAction(ActionType.InsertHtmlSignature, "Signature_Commerciale");

            // Assert
            Assert.Equal(ActionType.Forward, forwardAction.Type);
            Assert.Equal(ActionType.SetImportance, importanceAction.Type);
            Assert.Equal(ActionType.AddReminder, reminderAction.Type);
            Assert.Equal(ActionType.FlagToday, flagTodayAction.Type);
            Assert.Equal(ActionType.InsertHtmlSignature, signatureAction.Type);
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
        public void RuleCriterion_WithCategoryField_AndValidOperator_ShouldBeValid()
        {
            // Arrange & Act
            var criterion1 = new RuleCriterion(MailField.Category, MatchOperator.ExactNode, "Factures");
            var criterion2 = new RuleCriterion(MailField.Category, MatchOperator.NodeAndChildren, "Achats");

            // Assert
            Assert.Equal(MatchOperator.ExactNode, criterion1.Operator);
            Assert.Equal(MatchOperator.NodeAndChildren, criterion2.Operator);
        }

        [Fact]
        public void RuleCriterion_WithInvalidCombinations_ShouldThrowArgumentException()
        {
            // Un champ Category ne peut pas utiliser Contains, Equals ou RegexMatch
            Assert.Throws<ArgumentException>(() => new RuleCriterion(MailField.Category, MatchOperator.Contains, "Factures"));
            Assert.Throws<ArgumentException>(() => new RuleCriterion(MailField.Category, MatchOperator.Equals, "Factures"));

            // Un champ texte (ex: Subject) ne peut pas utiliser ExactNode ou NodeAndChildren
            Assert.Throws<ArgumentException>(() => new RuleCriterion(MailField.Subject, MatchOperator.ExactNode, "Urgent"));
            Assert.Throws<ArgumentException>(() => new RuleCriterion(MailField.Sender, MatchOperator.NodeAndChildren, "boss"));
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

        [Fact]
        public void RuleNode_SetOperator_ShouldUpdateOperatorValue()
        {
            // Arrange
            var node = new RuleNode(LogicalOperator.And);

            // Act
            node.SetOperator(LogicalOperator.Or);

            // Assert
            Assert.Equal(LogicalOperator.Or, node.Operator);
        }

        [Fact]
        public void RuleNode_UpdateCriterion_ShouldReplaceOldCriterionWithNewOne()
        {
            // Arrange
            var node = new RuleNode(LogicalOperator.And);
            var oldCriterion = new RuleCriterion(MailField.Subject, MatchOperator.Contains, "Old");
            var newCriterion = new RuleCriterion(MailField.Sender, MatchOperator.Equals, "New");
            node.AddCriterion(oldCriterion);

            // Act
            node.UpdateCriterion(oldCriterion, newCriterion);

            // Assert
            Assert.Single(node.Criteria);
            Assert.Equal(newCriterion, node.Criteria.First());
            Assert.DoesNotContain(oldCriterion, node.Criteria);
        }

        [Fact]
        public void ExecutionRule_Creation_ShouldSetPropertiesWithMultipleActions()
        {
            // Arrange
            var rootNode = new RuleNode(LogicalOperator.And);
            var action1 = new RuleAction(ActionType.SetImportance, "Haute");
            var action2 = new RuleAction(ActionType.MoveToFolder, "Archives");
            var actions = new List<RuleAction> { action1, action2 };

            // Act
            var rule = new ExecutionRule("Règle Multi-Actions", rootNode, actions);

            // Assert
            Assert.Equal("Règle Multi-Actions", rule.Name);
            Assert.Equal(rootNode, rule.RootNode);
            Assert.Equal(2, rule.Actions.Count);
            Assert.Equal(ActionType.SetImportance, rule.Actions[0].Type);
            Assert.Equal(ActionType.MoveToFolder, rule.Actions[1].Type);
        }
    }
}