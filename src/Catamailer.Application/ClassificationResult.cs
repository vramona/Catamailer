// Historique :
// 2026-09-07 : Création de la classe ClassificationResult (J1-S2-T1).

using System.Collections.Generic;
using System.Linq;
using Catamailer.Domain;

namespace Catamailer.Application
{
    /// <summary>
    /// Représente le résultat de l'évaluation de l'Étape 1 (Classification).
    /// </summary>
    public class ClassificationResult
    {
        /// <summary>
        /// Obtient la catégorie principale déduite par le moteur.
        /// </summary>
        public CategoryNode MatchedCategory { get; }

        /// <summary>
        /// Obtient la chaîne d'ascendance complète des catégories appliquées.
        /// </summary>
        public IReadOnlyList<CategoryNode> AppliedCategories { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ClassificationResult"/>.
        /// </summary>
        /// <param name="matchedCategory">La catégorie correspondante.</param>
        /// <param name="appliedCategories">La chaîne d'ascendance.</param>
        public ClassificationResult(CategoryNode matchedCategory, IEnumerable<CategoryNode> appliedCategories)
        {
            MatchedCategory = matchedCategory;
            AppliedCategories = appliedCategories.ToList().AsReadOnly();
        }
    }
}