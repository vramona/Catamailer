// Historique :
// 2026-09-07 : Création de l'objet de valeur RuleCriterion (J1-S1-T2).

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
        /// </summary>
        /// <param name="field">Le champ de l'e-mail.</param>
        /// <param name="operator">L'opérateur de comparaison.</param>
        /// <param name="value">La valeur cible recherchée.</param>
        public RuleCriterion(MailField field, MatchOperator @operator, string value)
        {
            Field = field;
            Operator = @operator;
            Value = value;
        }
    }
}