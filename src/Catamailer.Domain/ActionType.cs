// Historique :
// 2026-09-07 : Création de l'énumération ActionType (J1-S1-T2).

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente les types d'actions physiques réalisables sur un e-mail à l'issue de l'étape 2.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Déplace l'e-mail vers un dossier spécifique.
        /// </summary>
        MoveToFolder,
        
        /// <summary>
        /// Marque l'e-mail comme lu.
        /// </summary>
        MarkAsRead,
        
        /// <summary>
        /// Assure le suivi de l'e-mail (Drapeau).
        /// </summary>
        FlagForFollowUp
    }
}