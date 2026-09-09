// Historique :
// 2026-09-09 : Création de l'interface IRuleRepository (J3-S2-T2-ST2).

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
    }
}