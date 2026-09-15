// Historique :
// 2026-09-07 : Création de la classe ClassificationEngine (J1-S2-T1).
// 2026-09-07 : Modification pour retourner ClassificationResult et la chaîne d'ascendance (J1-S2-T1).
// 2026-09-07 : Ajout de la prise en compte des destinataires (J1-S2-T1).
// 2026-09-09 : Refonte pour consommer MailMetadata et gérer les listes distinctes de DictionaryRule (J3-S2-T2-ST1).

using System;
using System.Collections.Generic;
using System.Linq;
using Catamailer.Domain;

namespace Catamailer.Application
{
    /// <summary>
    /// Moteur d'évaluation de l'Étape 1 : déduction de la catégorie principale en fonction des dictionnaires de mots-clés contextuels.
    /// </summary>
    public class ClassificationEngine
    {
        /// <summary>
        /// Évalue les métadonnées d'un e-mail par rapport à un ensemble de règles de dictionnaire.
        /// </summary>
        /// <param name="metadata">Les métadonnées extraites de l'e-mail.</param>
        /// <param name="rules">La collection des règles de dictionnaire à évaluer.</param>
        /// <returns>Le résultat de la classification contenant la catégorie cible et son ascendance, ou null si aucune règle ne correspond.</returns>
        public ClassificationResult? Classify(MailMetadata metadata, IEnumerable<DictionaryRule> rules)
        {
            if (rules == null || !rules.Any() || metadata == null)
            {
                return null;
            }

            var sendersToMatch = new List<string> { metadata.Sender };
            if (!string.IsNullOrWhiteSpace(metadata.OnBehalfOf))
            {
                sendersToMatch.Add(metadata.OnBehalfOf);
            }

            var recipientsToMatch = new List<string>();
            if (metadata.InternalRecipients != null) recipientsToMatch.AddRange(metadata.InternalRecipients);
            if (metadata.ExternalRecipients != null) recipientsToMatch.AddRange(metadata.ExternalRecipients);

            var subject = metadata.Subject ?? string.Empty;

            foreach (var rule in rules)
            {
                bool isMatch = false;

                if (rule.SubjectKeywords.Any(k => subject.Contains(k, StringComparison.OrdinalIgnoreCase)))
                {
                    isMatch = true;
                }
                else if (rule.SenderKeywords.Any(k => sendersToMatch.Any(s => s != null && s.Contains(k, StringComparison.OrdinalIgnoreCase))))
                {
                    isMatch = true;
                }
                else if (rule.RecipientKeywords.Any(k => recipientsToMatch.Any(r => r != null && r.Contains(k, StringComparison.OrdinalIgnoreCase))))
                {
                    isMatch = true;
                }

                if (isMatch)
                {
                    var matchedCategory = rule.TargetCategory;
                    var ascendance = matchedCategory.GetAscendanceChain();
                    
                    return new ClassificationResult(matchedCategory, ascendance);
                }
            }

            return null;
        }
    }
}