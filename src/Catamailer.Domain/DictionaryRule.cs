// Historique :
// 2026-09-07 : Création de l'entité DictionaryRule (J1-S1-T2).

using System.Collections.Generic;
using System.Linq;

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente une règle de l'Étape 1 (Classification) liant un ensemble de mots-clés à une catégorie déduite.
    /// </summary>
    public class DictionaryRule
    {
        /// <summary>
        /// Obtient la catégorie cible qui sera déduite si la règle correspond.
        /// </summary>
        public CategoryNode TargetCategory { get; }

        /// <summary>
        /// Obtient la liste des mots-clés déclencheurs de cette règle.
        /// </summary>
        public IReadOnlyList<string> Keywords { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DictionaryRule"/>.
        /// </summary>
        /// <param name="targetCategory">La catégorie cible déduite.</param>
        /// <param name="keywords">Les mots-clés associés.</param>
        public DictionaryRule(CategoryNode targetCategory, IEnumerable<string> keywords)
        {
            TargetCategory = targetCategory;
            Keywords = keywords.ToList().AsReadOnly();
        }
    }
}