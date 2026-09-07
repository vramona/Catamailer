// Historique :
// 2026-09-07 : Création de la classe ClassificationEngine (J1-S2-T1).
// 2026-09-07 : Modification pour retourner ClassificationResult et la chaîne d'ascendance (J1-S2-T1).
// 2026-09-07 : Ajout de la prise en compte des destinataires (J1-S2-T1).

using System;
using System.Collections.Generic;
using System.Linq;
using Catamailer.Domain;

namespace Catamailer.Application
{
    /// <summary>
    /// Moteur d'évaluation de l'Étape 1 : déduction de la catégorie principale en fonction des dictionnaires de mots-clés.
    /// </summary>
    public class ClassificationEngine
    {
        /// <summary>
        /// Évalue les métadonnées d'un e-mail par rapport à un ensemble de règles de dictionnaire.
        /// </summary>
        /// <param name="subject">Le sujet de l'e-mail.</param>
        /// <param name="sender">L'adresse de l'expéditeur.</param>
        /// <param name="recipients">La liste des adresses des destinataires.</param>
        /// <param name="rules">La collection des règles de dictionnaire à évaluer.</param>
        /// <returns>Le résultat de la classification contenant la catégorie cible et son ascendance, ou null si aucune règle ne correspond.</returns>
        public ClassificationResult? Classify(string subject, string sender, IEnumerable<string> recipients, IEnumerable<DictionaryRule> rules)
        {
            if (rules == null || !rules.Any())
            {
                return null;
            }

            var recipientsText = recipients != null ? string.Join(" ", recipients) : string.Empty;
            var searchableText = $"{subject} {sender} {recipientsText}";

            foreach (var rule in rules)
            {
                foreach (var keyword in rule.Keywords)
                {
                    if (searchableText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        var matchedCategory = rule.TargetCategory;
                        var ascendance = matchedCategory.GetAscendanceChain();
                        
                        return new ClassificationResult(matchedCategory, ascendance);
                    }
                }
            }

            return null;
        }
    }
}