// Historique :
// 2026-09-07 : Création de l'entité DictionaryRule (J1-S1-T2).
// 2026-09-09 : Scission de Keywords en Subject/Sender/Recipient pour classification ciblée (J3-S2-T2-ST1).

using System.Collections.Generic;
using System.Linq;

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente une règle de l'Étape 1 (Classification) liant des mots-clés spécifiques (Sujet, Expéditeur, Destinataire) à une catégorie déduite.
    /// </summary>
    public class DictionaryRule
    {
        /// <summary>
        /// Obtient la catégorie cible qui sera déduite si la règle correspond.
        /// </summary>
        public CategoryNode TargetCategory { get; }

        /// <summary>
        /// Obtient la liste des mots-clés recherchés dans le sujet.
        /// </summary>
        public IReadOnlyList<string> SubjectKeywords { get; }

        /// <summary>
        /// Obtient la liste des mots-clés (adresses ou noms) recherchés parmi les expéditeurs.
        /// </summary>
        public IReadOnlyList<string> SenderKeywords { get; }

        /// <summary>
        /// Obtient la liste des mots-clés (adresses ou noms) recherchés parmi les destinataires.
        /// </summary>
        public IReadOnlyList<string> RecipientKeywords { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="DictionaryRule"/>.
        /// </summary>
        /// <param name="targetCategory">La catégorie cible déduite.</param>
        /// <param name="subjectKeywords">Les mots-clés liés au sujet.</param>
        /// <param name="senderKeywords">Les mots-clés liés aux expéditeurs.</param>
        /// <param name="recipientKeywords">Les mots-clés liés aux destinataires.</param>
        public DictionaryRule(
            CategoryNode targetCategory, 
            IEnumerable<string>? subjectKeywords = null,
            IEnumerable<string>? senderKeywords = null,
            IEnumerable<string>? recipientKeywords = null)
        {
            TargetCategory = targetCategory;
            SubjectKeywords = (subjectKeywords ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
            SenderKeywords = (senderKeywords ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
            RecipientKeywords = (recipientKeywords ?? Enumerable.Empty<string>()).ToList().AsReadOnly();
        }
    }
}