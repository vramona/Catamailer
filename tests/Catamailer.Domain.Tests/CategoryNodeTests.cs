// Historique :
// 2026-09-07 : Création de la classe de test pour la modélisation CategoryNode.
// 2026-09-21 : Ajout des tests pour la suppression logique IsDeleted (J4-S4-T6).

using System;
using System.Linq;
using Xunit;

namespace Catamailer.Domain.Tests
{
    /// <summary>
    /// Classe de test validant les règles métier de l'arbre des catégories (CategoryNode).
    /// </summary>
    public class CategoryNodeTests
    {
        [Fact]
        public void CategoryNode_Creation_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var node = new CategoryNode("CCOEN", "#FF0000");

            // Assert
            Assert.Equal("CCOEN", node.Name);
            Assert.Equal("#FF0000", node.Color);
            Assert.Equal("#FF0000", node.EffectiveColor);
            Assert.Null(node.Parent);
            Assert.Empty(node.Children);
        }

        [Fact]
        public void AddChild_ShouldSetParentAndInheritColor_WhenNoColorSpecified()
        {
            // Arrange
            var root = new CategoryNode("CCOEN", "#FF0000");
            var child = new CategoryNode("Voyage"); // Aucune couleur explicite

            // Act
            root.AddChild(child);

            // Assert
            Assert.Single(root.Children);
            Assert.Equal(root, child.Parent);
            Assert.Null(child.Color);
            Assert.Equal("#FF0000", child.EffectiveColor);
        }

        [Fact]
        public void EffectiveColor_ShouldOverrideParentColor_WhenColorIsExplicitlySet()
        {
            // Arrange
            var root = new CategoryNode("CCOEN", "#FF0000");
            var child = new CategoryNode("Urgent", "#00FF00");

            // Act
            root.AddChild(child);

            // Assert
            Assert.Equal("#00FF00", child.Color);
            Assert.Equal("#00FF00", child.EffectiveColor);
        }
        
        [Fact]
        public void GetAscendanceChain_ShouldReturnFullHierarchy_FromRootToNode()
        {
            // Arrange
            var root = new CategoryNode("CCOEN");
            var subCategory = new CategoryNode("Projet");
            var leaf = new CategoryNode("Dev");
            
            root.AddChild(subCategory);
            subCategory.AddChild(leaf);

            // Act
            var chain = leaf.GetAscendanceChain().ToList();

            // Assert
            Assert.Equal(3, chain.Count);
            Assert.Equal("CCOEN", chain[0].Name);
            Assert.Equal("Projet", chain[1].Name);
            Assert.Equal("Dev", chain[2].Name);
        }

        [Fact]
        public void CategoryNode_Creation_ShouldSetIsDeletedToFalse()
        {
            // Arrange & Act
            var node = new CategoryNode("Test");

            // Assert
            Assert.False(node.IsDeleted);
        }

        [Fact]
        public void MarkAsDeleted_ShouldSetIsDeletedToTrue()
        {
            // Arrange
            var node = new CategoryNode("Test");

            // Act
            node.MarkAsDeleted();

            // Assert
            Assert.True(node.IsDeleted);
        }

        [Fact]
        public void Restore_ShouldSetIsDeletedToFalse()
        {
            // Arrange
            var node = new CategoryNode("Test");
            node.MarkAsDeleted();

            // Act
            node.Restore();

            // Assert
            Assert.False(node.IsDeleted);
        }
    }
}