// Historique :
// 2026-09-07 : Création de l'implémentation CategoryRepository (J1-S1-T3).
// 2026-09-08 : Implémentation de GetAllAsync (J3-S2-T1).
// 2026-09-08 : Ajout du chargement explicite (Include) du Parent dans GetAllAsync (J3-S2-T1).

using System.Collections.Generic;
using System.Threading.Tasks;
using Catamailer.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catamailer.Infrastructure
{
    /// <summary>
    /// Implémentation SQLite du dépôt pour les catégories utilisant Entity Framework Core.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatamailerDbContext _context;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CategoryRepository"/>.
        /// </summary>
        /// <param name="context">Le contexte de base de données EF Core.</param>
        public CategoryRepository(CatamailerDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddAsync(CategoryNode category)
        {
            await _context.Categories.AddAsync(category);
        }

        /// <inheritdoc />
        public async Task<CategoryNode?> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CategoryNode>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Parent)
                .ToListAsync();
        }
    }
}