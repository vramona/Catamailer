// Historique :
// 2026-09-07 : Création des tests pour CategoryRepository (J1-S1-T3).
// 2026-09-23 : Ajout des tests pour AddRangeAsync et UpdateRangeAsync (J4-S4-T7 - Phase Rouge).

using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public async Task AddRangeAsync_ShouldPersistMultipleCategories_InOneTransaction()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var repository = new CategoryRepository(context);
            var categories = new List<CategoryNode>
            {
                new CategoryNode("Cat1", "#111111"),
                new CategoryNode("Cat2", "#222222")
            };

            // Act
            await repository.AddRangeAsync(categories);

            // Assert
            var allCategories = await context.Categories.ToListAsync();
            Assert.Equal(2, allCategories.Count);
            Assert.Contains(allCategories, c => c.Name == "Cat1");
            Assert.Contains(allCategories, c => c.Name == "Cat2");
        }

        [Fact]
        public async Task UpdateRangeAsync_ShouldUpdateMultipleCategories_InOneTransaction()
        {
            // Arrange
            using var context = GetInMemoryContext();
            var cat1 = new CategoryNode("Cat1", "#111111");
            var cat2 = new CategoryNode("Cat2", "#222222");
            context.Categories.AddRange(cat1, cat2);
            await context.SaveChangesAsync();
            
            var repository = new CategoryRepository(context);

            // Act
            cat1.UpdateColor("#AAAAAA");
            cat2.UpdateColor("#BBBBBB");
            await repository.UpdateRangeAsync(new[] { cat1, cat2 });

            // Assert
            var updatedCat1 = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Cat1");
            var updatedCat2 = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Cat2");
            Assert.NotNull(updatedCat1);
            Assert.NotNull(updatedCat2);
            Assert.Equal("#AAAAAA", updatedCat1.Color);
            Assert.Equal("#BBBBBB", updatedCat2.Color);
        }
    }
}