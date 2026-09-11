// Historique :
// 2026-09-09 : Création de l'interface IRuleRepository (J3-S2-T2-ST2).
// 2026-09-11 : Ajout des méthodes AddDictionaryRuleAsync et DeleteDictionaryRuleAsync (J3-S3-T3-ST1).

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catamailer.Domain
{
    /// <summary>
    /// Définit le contrat pour l'accès aux données des règles de classification et d'exécution.
    /// </summary>
    public interface IRuleRepository
    {
        /// <summary>
        /// Récupère l'intégralité des règles de dictionnaire (Étape 1).
        /// </summary>
        /// <returns>Une collection de DictionaryRule.</returns>
        Task<IEnumerable<DictionaryRule>> GetAllDictionaryRulesAsync();

        /// <summary>
        /// Ajoute une nouvelle règle de dictionnaire.
        /// </summary>
        /// <param name="rule">La règle à ajouter.</param>
        Task AddDictionaryRuleAsync(DictionaryRule rule);

        /// <summary>
        /// Supprime une règle de dictionnaire existante.
        /// </summary>
        /// <param name="rule">La règle à supprimer.</param>
        Task DeleteDictionaryRuleAsync(DictionaryRule rule);
    }
}