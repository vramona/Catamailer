// Historique :
// 2026-09-07 : Création de l'énumération MatchOperator (J1-S1-T2).
// 2026-09-11 : Ajout des opérateurs pour l'arbre des catégories (J3-S3-T3-ST2 - Phase Verte).

namespace Catamailer.Domain
{
    /// <summary>
    /// Définit les opérateurs de correspondance applicables aux critères d'évaluation.
    /// </summary>
    public enum MatchOperator
    {
        /// <summary>
        /// Correspondance stricte et exacte (applicable aux chaînes).
        /// </summary>
        Equals,
        
        /// <summary>
        /// Correspondance partielle (la chaîne cible contient la valeur).
        /// </summary>
        Contains,
        
        /// <summary>
        /// Correspondance basée sur une expression régulière.
        /// </summary>
        RegexMatch,

        /// <summary>
        /// Correspondance stricte au nœud de catégorie ciblé (sans ses enfants).
        /// </summary>
        ExactNode,

        /// <summary>
        /// Correspondance au nœud de catégorie ciblé ou à n'importe lequel de ses enfants (ascendance).
        /// </summary>
        NodeAndChildren
    }
}