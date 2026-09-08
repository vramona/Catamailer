// Historique :
// 2026-09-07 : Création de l'interface ICategoryRepository (J1-S1-T3).
// 2026-09-08 : Ajout de la méthode GetAllAsync pour le moteur de recherche (J3-S2-T1).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catamailer.Domain
{
    /// <summary>
    /// Définit le contrat pour l'accès aux données de l'entité CategoryNode.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Ajoute une nouvelle catégorie au dépôt de données.
        /// </summary>
        /// <param name="category">La catégorie à ajouter.</param>
        /// <returns>Une tâche asynchrone représentant l'opération.</returns>
        Task AddAsync(CategoryNode category);

        /// <summary>
        /// Récupère une catégorie par son nom (identifiant unique de la nomenclature Master Data).
        /// </summary>
        /// <param name="name">Le nom de la catégorie.</param>
        /// <returns>La catégorie correspondante, ou null si elle n'existe pas.</returns>
        Task<CategoryNode?> GetByNameAsync(string name);

        /// <summary>
        /// Récupère l'intégralité des catégories existantes.
        /// </summary>
        /// <returns>Une collection de toutes les catégories.</returns>
        Task<IEnumerable<CategoryNode>> GetAllAsync();
    }
}