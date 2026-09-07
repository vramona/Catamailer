// Historique :
// 2026-09-07 : Création de l'objet de valeur RuleAction (J1-S1-T2).

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente l'action finale à exécuter si un arbre de conditions est validé.
    /// </summary>
    public class RuleAction
    {
        /// <summary>
        /// Obtient le type de l'action à exécuter.
        /// </summary>
        public ActionType Type { get; }

        /// <summary>
        /// Obtient le paramètre associé à l'action (par exemple, le nom du dossier cible).
        /// </summary>
        public string Parameter { get; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="RuleAction"/>.
        /// </summary>
        /// <param name="type">Le type d'action.</param>
        /// <param name="parameter">Le paramètre de configuration de l'action.</param>
        public RuleAction(ActionType type, string parameter)
        {
            Type = type;
            Parameter = parameter;
        }
    }
}