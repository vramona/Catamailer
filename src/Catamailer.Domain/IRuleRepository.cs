// Historique :
// 2026-09-09 : Création de l'interface IRuleRepository (J3-S2-T2-ST2).
// 2026-09-11 : Ajout des méthodes AddDictionaryRuleAsync et DeleteDictionaryRuleAsync (J3-S3-T3-ST1).
// 2026-09-11 : Ajout des méthodes pour ExecutionRule (J3-S3-T3-ST2 - Phase Verte).
// 2026-09-25 : Ajout de UpdateDictionaryRuleAsync pour l'édition (J6-S3-T6 - Phase Rouge).

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
        /// Met à jour une règle de dictionnaire existante.
        /// </summary>
        /// <param name="rule">La règle à mettre à jour.</param>
        Task UpdateDictionaryRuleAsync(DictionaryRule rule);

        /// <summary>
        /// Supprime une règle de dictionnaire existante.
        /// </summary>
        /// <param name="rule">La règle à supprimer.</param>
        Task DeleteDictionaryRuleAsync(DictionaryRule rule);

        /// <summary>
        /// Récupère l'intégralité des règles d'exécution (Étape 2).
        /// </summary>
        /// <returns>Une collection de ExecutionRule.</returns>
        Task<IEnumerable<ExecutionRule>> GetAllExecutionRulesAsync();

        /// <summary>
        /// Ajoute une nouvelle règle d'exécution.
        /// </summary>
        /// <param name="rule">La règle à ajouter.</param>
        Task AddExecutionRuleAsync(ExecutionRule rule);

        /// <summary>
        /// Supprime une règle d'exécution existante.
        /// </summary>
        /// <param name="rule">La règle à supprimer.</param>
        Task DeleteExecutionRuleAsync(ExecutionRule rule);
    }
}