// Historique :
// 2026-09-07 : Création de l'énumération MailField (J1-S1-T2).

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente les champs d'un e-mail pouvant être analysés par le moteur d'exécution.
    /// </summary>
    public enum MailField
    {
        /// <summary>
        /// Le sujet de l'e-mail.
        /// </summary>
        Subject,
        
        /// <summary>
        /// L'expéditeur de l'e-mail.
        /// </summary>
        Sender,
        
        /// <summary>
        /// Les destinataires de l'e-mail (To, Cc, Cci).
        /// </summary>
        Recipients,
        
        /// <summary>
        /// Le corps textuel de l'e-mail.
        /// </summary>
        Body
    }
}