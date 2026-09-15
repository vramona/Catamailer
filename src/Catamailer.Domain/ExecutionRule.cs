// Historique :
// 2026-09-11 : Création de l'entité ExecutionRule (J3-S3-T3-ST2 - Phase Verte).

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
        /// Obtient l'action finale à exécuter si l'arbre est validé.
        /// </summary>
        public RuleAction Action { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ExecutionRule"/>.
        /// </summary>
        /// <param name="name">Le nom de la règle.</param>
        /// <param name="rootNode">Le nœud composite racine.</param>
        /// <param name="action">L'action à appliquer.</param>
        public ExecutionRule(string name, RuleNode rootNode, RuleAction action)
        {
            Name = name;
            RootNode = rootNode;
            Action = action;
        }
    }
}