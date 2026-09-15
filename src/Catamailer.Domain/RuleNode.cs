// Historique :
// 2026-09-07 : Création de l'entité RuleNode (J1-S1-T2).
// 2026-09-11 : Ajout des méthodes de suppression (J3-S3-T3-ST2 - Phase Verte).
// 2026-09-11 : Ajout des méthodes de mutation pour l'interface (J3-S3-T3-ST2 - Phase Orange/Verte).

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
        public LogicalOperator Operator { get; private set; }

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
        /// Modifie l'opérateur logique de ce nœud.
        /// </summary>
        /// <param name="newOperator">Le nouvel opérateur logique.</param>
        public void SetOperator(LogicalOperator newOperator)
        {
            Operator = newOperator;
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
        /// Met à jour un critère existant en le remplaçant par un nouveau (Value Object).
        /// </summary>
        /// <param name="oldCriterion">Le critère à remplacer.</param>
        /// <param name="newCriterion">Le nouveau critère.</param>
        public void UpdateCriterion(RuleCriterion oldCriterion, RuleCriterion newCriterion)
        {
            var index = _criteria.IndexOf(oldCriterion);
            if (index >= 0)
            {
                _criteria[index] = newCriterion;
            }
        }

        /// <summary>
        /// Supprime un critère de validation unitaire de ce nœud.
        /// </summary>
        /// <param name="criterion">Le critère à supprimer.</param>
        public void RemoveCriterion(RuleCriterion criterion)
        {
            _criteria.Remove(criterion);
        }

        /// <summary>
        /// Ajoute un nœud enfant permettant d'imbriquer une nouvelle couche logique.
        /// </summary>
        /// <param name="childNode">Le nœud composite enfant.</param>
        public void AddChildNode(RuleNode childNode)
        {
            _childNodes.Add(childNode);
        }

        /// <summary>
        /// Supprime un nœud enfant de la couche logique.
        /// </summary>
        /// <param name="childNode">Le nœud composite enfant à supprimer.</param>
        public void RemoveChildNode(RuleNode childNode)
        {
            _childNodes.Remove(childNode);
        }
    }
}