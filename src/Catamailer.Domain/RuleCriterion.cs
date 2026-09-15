// Historique :
// 2026-09-07 : Création de l'objet de valeur RuleCriterion (J1-S1-T2).
// 2026-09-11 : Ajout des Guard clauses pour la validation croisée Field/Operator (J3-S3-T3-ST2 - Phase Verte).

using System;

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente un critère de filtrage unitaire dans l'arbre d'exécution de l'Étape 2.
    /// </summary>
    public class RuleCriterion
    {
        /// <summary>
        /// Obtient le champ de l'e-mail à analyser.
        /// </summary>
        public MailField Field { get; }

        /// <summary>
        /// Obtient l'opérateur de comparaison.
        /// </summary>
        public MatchOperator Operator { get; }

        /// <summary>
        /// Obtient la valeur cible de la condition.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RuleCriterion"/>.
        /// Lève une exception si la combinaison Field et Operator est illégale.
        /// </summary>
        /// <param name="field">Le champ de l'e-mail.</param>
        /// <param name="operator">L'opérateur de comparaison.</param>
        /// <param name="value">La valeur cible recherchée.</param>
        /// <exception cref="ArgumentException">Si la combinaison champ/opérateur est invalide.</exception>
        public RuleCriterion(MailField field, MatchOperator @operator, string value)
        {
            ValidateCombination(field, @operator);

            Field = field;
            Operator = @operator;
            Value = value;
        }

        private static void ValidateCombination(MailField field, MatchOperator @operator)
        {
            if (field == MailField.Category)
            {
                if (@operator != MatchOperator.ExactNode && @operator != MatchOperator.NodeAndChildren)
                {
                    throw new ArgumentException($"L'opérateur {@operator} n'est pas autorisé pour le champ Category.");
                }
            }
            else
            {
                if (@operator == MatchOperator.ExactNode || @operator == MatchOperator.NodeAndChildren)
                {
                    throw new ArgumentException($"L'opérateur {@operator} est exclusif au champ Category.");
                }
            }
        }
    }
}