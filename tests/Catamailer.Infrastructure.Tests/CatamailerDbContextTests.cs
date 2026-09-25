// Historique :
// 2026-09-07 : Création des tests pour CatamailerDbContext (J1-S1-T3).
// 2026-09-22 : Ajout des tests pour le filtre global IsDeleted (J4-S4-T6).
// 2026-09-24 : Ajout du test CanSaveAndRetrieve_DictionaryRule (J6-S2-T1 - Phase Rouge).

using System.Collections.Generic;
using System.Linq;
using Catamailer.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catamailer.Infrastructure.Tests
{
    /// <summary>
    /// Classe de test validant l'intégration d'Entity Framework Core SQLite.
    /// </summary>
    public class CatamailerDbContextTests
    {
        [Fact]
        public void EnsureCreated_ShouldCreateDatabaseAndTables()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CatamailerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new CatamailerDbContext(options);
            
            // Pour SQLite in-memory, il faut ouvrir la connexion manuellement
            // pour que la base de données ne soit pas détruite prématurément.
            context.Database.OpenConnection();

            // Act
            var created = context.Database.EnsureCreated();

            // Assert
            Assert.True(created);
            Assert.Empty(context.Categories);
        }

        [Fact]
        public void CanSaveAndRetrieve_CategoryNode()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CatamailerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new CatamailerDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var category = new CategoryNode("Urgent", "#FF0000");

            // Act
            context.Categories.Add(category);
            context.SaveChanges();

            // Assert
            var retrieved = context.Categories.FirstOrDefault(c => c.Name == "Urgent");
            Assert.NotNull(retrieved);
            Assert.Equal("#FF0000", retrieved.Color);
        }

        [Fact]
        public void GlobalQueryFilter_ShouldHideDeletedCategories_ByDefault()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CatamailerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new CatamailerDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var activeCategory = new CategoryNode("Active");
            var deletedCategory = new CategoryNode("Deleted");
            deletedCategory.MarkAsDeleted();

            context.Categories.Add(activeCategory);
            context.Categories.Add(deletedCategory);
            context.SaveChanges();

            // Act
            var allCategories = context.Categories.ToList();

            // Assert
            Assert.Single(allCategories);
            Assert.Equal("Active", allCategories.First().Name);
        }

        [Fact]
        public void IgnoreQueryFilters_ShouldReturnDeletedCategories()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CatamailerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new CatamailerDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var activeCategory = new CategoryNode("Active");
            var deletedCategory = new CategoryNode("Deleted");
            deletedCategory.MarkAsDeleted();

            context.Categories.Add(activeCategory);
            context.Categories.Add(deletedCategory);
            context.SaveChanges();

            // Act
            var allCategories = context.Categories.IgnoreQueryFilters().ToList();

            // Assert
            Assert.Equal(2, allCategories.Count);
            Assert.Contains(allCategories, c => c.Name == "Deleted");
        }

        [Fact]
        public void CanSaveAndRetrieve_DictionaryRule()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CatamailerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            using var context = new CatamailerDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();

            var category = new CategoryNode("Urba");
            var rule = new DictionaryRule(
                category,
                new List<string> { "Architecture" },
                new List<string> { "architecte@domaine.fr" },
                new List<string> { "equipe-urba@domaine.fr" }
            );

            // Act
            context.Categories.Add(category);
            // On utilise Set<DictionaryRule>() car le DbSet n'est pas encore déclaré dans la Phase Rouge
            context.Set<DictionaryRule>().Add(rule);
            context.SaveChanges();

            // Assert
            var retrieved = context.Set<DictionaryRule>()
                .Include(r => r.TargetCategory)
                .FirstOrDefault();
                
            Assert.NotNull(retrieved);
            Assert.Equal("Urba", retrieved.TargetCategory.Name);
            Assert.Single(retrieved.SubjectKeywords);
            Assert.Equal("Architecture", retrieved.SubjectKeywords[0]);
            Assert.Single(retrieved.SenderKeywords);
            Assert.Equal("architecte@domaine.fr", retrieved.SenderKeywords[0]);
            Assert.Single(retrieved.RecipientKeywords);
            Assert.Equal("equipe-urba@domaine.fr", retrieved.RecipientKeywords[0]);
        }
    }
}