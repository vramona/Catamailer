// Historique :
// 2026-09-07 : Création de l'entité RuleNode (J1-S1-T2).

using System.Collections.Generic;

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente un nœud dans l'arbre composite des conditions d'exécution de l'Étape 2.
    /// </summary>
    public class RuleNode
    {
        private readonly List<RuleCriterion> _criteria = new();
        private readonly List<RuleNode> _childNodes = new();

        /// <summary>
        /// Obtient l'opérateur logique liant les enfants de ce nœud.
        /// </summary>
        public LogicalOperator Operator { get; }

        /// <summary>
        /// Obtient la liste en lecture seule des critères associés à ce nœud.
        /// </summary>
        public IReadOnlyList<RuleCriterion> Criteria => _criteria.AsReadOnly();

        /// <summary>
        /// Obtient la liste en lecture seule des nœuds enfants (sous-arbres).
        /// </summary>
        public IReadOnlyList<RuleNode> ChildNodes => _childNodes.AsReadOnly();

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RuleNode"/>.
        /// </summary>
        /// <param name="operator">L'opérateur logique du nœud composite.</param>
        public RuleNode(LogicalOperator @operator)
        {
            Operator = @operator;
        }

        /// <summary>
        /// Ajoute un critère de validation unitaire à ce nœud.
        /// </summary>
        /// <param name="criterion">Le critère à ajouter.</param>
        public void AddCriterion(RuleCriterion criterion)
        {
            _criteria.Add(criterion);
        }

        /// <summary>
        /// Ajoute un nœud enfant permettant d'imbriquer une nouvelle couche logique.
        /// </summary>
        /// <param name="childNode">Le nœud composite enfant.</param>
        public void AddChildNode(RuleNode childNode)
        {
            _childNodes.Add(childNode);
        }
    }
}