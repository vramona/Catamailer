// Historique :
// 2026-09-07 : Création de l'énumération LogicalOperator (J1-S1-T2).

namespace Catamailer.Domain
{
    /// <summary>
    /// Définit les opérateurs logiques liant les critères ou les nœuds enfants dans l'arbre d'exécution.
    /// </summary>
    public enum LogicalOperator
    {
        /// <summary>
        /// Opérateur logique ET (toutes les conditions doivent être vraies).
        /// </summary>
        And,
        
        /// <summary>
        /// Opérateur logique OU (au moins une condition doit être vraie).
        /// </summary>
        Or
    }
}