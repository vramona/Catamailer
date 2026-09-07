// Historique :
// 2026-09-07 : Création des tests pour CategoryRepository (J1-S1-T3).

using System.Threading.Tasks;
using Catamailer.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catamailer.Infrastructure.Tests
{
    /// <summary>
    /// Classe de test validant le comportement du dépôt (Repository) des catégories.
    /// </summary>
    public class CategoryRepositoryTests
    {
        private CatamailerDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<CatamailerDbContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new CatamailerDbContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public async Task AddAsync_ShouldPersistCategory()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new CategoryRepository(context);
            var category = new CategoryNode("Projet Alpha", "#0000FF");

            // Act
            await repository.AddAsync(category);
            await context.SaveChangesAsync();

            // Assert
            var retrieved = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Projet Alpha");
            Assert.NotNull(retrieved);
            Assert.Equal("#0000FF", retrieved.Color);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnCategory_WhenExists()
        {
            // Arrange
            using var context = GetInMemoryContext();
            context.Categories.Add(new CategoryNode("Urgent", "#FF0000"));
            await context.SaveChangesAsync();
            var repository = new CategoryRepository(context);

            // Act
            var retrieved = await repository.GetByNameAsync("Urgent");

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal("Urgent", retrieved.Name);
        }
    }
}