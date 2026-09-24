// Historique :
// 2026-09-11 : Création de l'entité ExecutionRule (J3-S3-T3-ST2 - Phase Verte).
// 2026-09-24 : Remplacement de l'action unique par une collection d'actions (J5-S3-T1 - Phase Verte).
// 2026-09-24 : Correction du constructeur pour le binding EF Core JSON (J5-S3-T1 - Phase Orange).

using System.Collections.Generic;

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente l'entité racine (Aggregate Root) pour une règle de l'Étape 2.
    /// </summary>
    public class ExecutionRule
    {
        /// <summary>
        /// Obtient le nom descriptif de la règle.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Obtient le nœud racine de l'arbre des conditions d'exécution.
        /// </summary>
        public RuleNode RootNode { get; }

        /// <summary>
        /// Obtient la liste ordonnée des actions à exécuter si l'arbre est validé.
        /// </summary>
        public IReadOnlyList<RuleAction> Actions { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ExecutionRule"/> avec une liste d'actions séquentielles.
        /// Le type IReadOnlyList est utilisé pour garantir la compatibilité de binding avec Entity Framework Core.
        /// </summary>
        /// <param name="name">Le nom de la règle.</param>
        /// <param name="rootNode">Le nœud composite racine.</param>
        /// <param name="actions">La collection d'actions à appliquer.</param>
        public ExecutionRule(string name, RuleNode rootNode, IReadOnlyList<RuleAction> actions)
        {
            Name = name;
            RootNode = rootNode;
            Actions = actions;
        }
    }
}