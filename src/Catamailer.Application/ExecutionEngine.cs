// Historique :
// 2026-09-07 : Création de la classe ExecutionEngine (J1-S2-T2).
// 2026-09-07 : Ajout de l'évaluation des critères d'égalité et de la récursivité des nœuds logiques (J1-S2-T2).
// 2026-09-07 : Ajout de la prise en charge de l'opérateur Contains (J1-S2-T2).

using System;
using System.Linq;
using Catamailer.Domain;

namespace Catamailer.Application
{
    /// <summary>
    /// Moteur d'exécution de l'Étape 2 : évaluation de l'arbre booléen des conditions.
    /// </summary>
    public class ExecutionEngine
    {
        /// <summary>
        /// Évalue un nœud de règle composite par rapport aux métadonnées d'un e-mail.
        /// </summary>
        /// <param name="subject">Le sujet de l'e-mail.</param>
        /// <param name="sender">L'adresse de l'expéditeur.</param>
        /// <param name="node">Le nœud composite à évaluer.</param>
        /// <returns>True si l'arbre est validé, False sinon.</returns>
        public bool Evaluate(string subject, string sender, RuleNode node)
        {
            if (node == null || (!node.Criteria.Any() && !node.ChildNodes.Any()))
            {
                return false;
            }

            if (node.Operator == LogicalOperator.And)
            {
                // Pour un opérateur ET, tous les critères et nœuds enfants doivent être vrais.
                foreach (var criterion in node.Criteria)
                {
                    if (!EvaluateCriterion(subject, sender, criterion))
                        return false;
                }

                foreach (var childNode in node.ChildNodes)
                {
                    if (!Evaluate(subject, sender, childNode))
                        return false;
                }

                return true;
            }
            else // LogicalOperator.Or
            {
                // Pour un opérateur OU, au moins un critère ou nœud enfant doit être vrai.
                foreach (var criterion in node.Criteria)
                {
                    if (EvaluateCriterion(subject, sender, criterion))
                        return true;
                }

                foreach (var childNode in node.ChildNodes)
                {
                    if (Evaluate(subject, sender, childNode))
                        return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Évalue un critère unitaire par rapport aux métadonnées de l'e-mail.
        /// </summary>
        private bool EvaluateCriterion(string subject, string sender, RuleCriterion criterion)
        {
            string valueToTest = criterion.Field switch
            {
                MailField.Subject => subject ?? string.Empty,
                MailField.Sender => sender ?? string.Empty,
                _ => string.Empty
            };

            return criterion.Operator switch
            {
                MatchOperator.Equals => string.Equals(valueToTest, criterion.Value, StringComparison.OrdinalIgnoreCase),
                MatchOperator.Contains => valueToTest.Contains(criterion.Value, StringComparison.OrdinalIgnoreCase),
                _ => false // L'opérateur RegexMatch pourra être implémenté ultérieurement si nécessaire.
            };
        }
    }
}