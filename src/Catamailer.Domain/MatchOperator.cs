// Historique :
// 2026-09-07 : Création de l'énumération MatchOperator (J1-S1-T2).

namespace Catamailer.Domain
{
    /// <summary>
    /// Définit les opérateurs de correspondance applicables aux critères d'évaluation.
    /// </summary>
    public enum MatchOperator
    {
        /// <summary>
        /// Correspondance stricte et exacte.
        /// </summary>
        Equals,
        
        /// <summary>
        /// Correspondance partielle (la chaîne cible contient la valeur).
        /// </summary>
        Contains,
        
        /// <summary>
        /// Correspondance basée sur une expression régulière.
        /// </summary>
        RegexMatch
    }
}