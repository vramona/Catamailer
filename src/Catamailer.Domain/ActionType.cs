// Historique :
// 2026-09-07 : Création de l'énumération ActionType (J1-S1-T2).
// 2026-09-23 : Ajout des actions de traitement avancées pour la parité VBA (J5-S1-T1).

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
        /// Assure le suivi de l'e-mail (Drapeau classique).
        /// </summary>
        FlagForFollowUp,

        /// <summary>
        /// Transfère l'e-mail à un ou plusieurs destinataires.
        /// </summary>
        Forward,

        /// <summary>
        /// Définit le niveau d'importance de l'e-mail (ex: Haute, Normale, Faible).
        /// </summary>
        SetImportance,

        /// <summary>
        /// Ajoute un rappel à une date/heure spécifique.
        /// </summary>
        AddReminder,

        /// <summary>
        /// Assure un suivi pour aujourd'hui (Drapeau de tâche Outlook).
        /// </summary>
        FlagToday,

        /// <summary>
        /// Insère une signature HTML spécifique lors d'un transfert ou d'une réponse.
        /// </summary>
        InsertHtmlSignature
    }
}