// Historique :
// 2026-09-07 : Création de l'entité DictionaryRule (J1-S1-T2).
// 2026-09-09 : Scission de Keywords en Subject/Sender/Recipient pour classification ciblée (J3-S2-T2-ST1).
// 2026-09-25 : Ajout du constructeur privé et setters pour binding Entity Framework (J6-S2-T1 - Phase Verte).
// 2026-09-25 : Ajout de la méthode Update pour l'édition (J6-S3-T6 - Phase Rouge Correction).

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
        public CategoryNode TargetCategory { get; private set; } = null!;

        /// <summary>
        /// Obtient la liste des mots-clés recherchés dans le sujet.
        /// </summary>
        public IReadOnlyList<string> SubjectKeywords { get; private set; } = new List<string>();

        /// <summary>
        /// Obtient la liste des mots-clés (adresses ou noms) recherchés parmi les expéditeurs.
        /// </summary>
        public IReadOnlyList<string> SenderKeywords { get; private set; } = new List<string>();

        /// <summary>
        /// Obtient la liste des mots-clés (adresses ou noms) recherchés parmi les destinataires.
        /// </summary>
        public IReadOnlyList<string> RecipientKeywords { get; private set; } = new List<string>();

        /// <summary>
        /// Constructeur privé requis pour la matérialisation par Entity Framework Core.
        /// </summary>
        private DictionaryRule()
        {
        }

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
            Update(targetCategory, subjectKeywords, senderKeywords, recipientKeywords);
        }

        /// <summary>
        /// Met à jour l'ensemble des propriétés de la règle.
        /// </summary>
        /// <param name="targetCategory">La nouvelle catégorie cible déduite.</param>
        /// <param name="subjectKeywords">Les nouveaux mots-clés liés au sujet.</param>
        /// <param name="senderKeywords">Les nouveaux mots-clés liés aux expéditeurs.</param>
        /// <param name="recipientKeywords">Les nouveaux mots-clés liés aux destinataires.</param>
        public void Update(
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