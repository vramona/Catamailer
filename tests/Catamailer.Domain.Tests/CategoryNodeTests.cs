// Historique :
// 2026-09-07 : Création de la classe de test pour la modélisation CategoryNode.

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
    }
}