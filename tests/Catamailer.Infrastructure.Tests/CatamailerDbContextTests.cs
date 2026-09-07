// Historique :
// 2026-09-07 : Création des tests pour CatamailerDbContext (J1-S1-T3).

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
    }
}