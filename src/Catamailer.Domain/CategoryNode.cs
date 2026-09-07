// Historique :
// 2026-09-07 : Création de l'entité CategoryNode (J1-S1-T1).
// 2026-09-07 : Ajout du constructeur protégé pour compatibilité avec Entity Framework Core (J1-S1-T3).

using System.Collections.Generic;
using System.Linq;

namespace Catamailer.Domain
{
    /// <summary>
    /// Représente un nœud dans l'arbre hiérarchique des catégories, agissant comme vérité absolue (Master Data Management).
    /// </summary>
    public class CategoryNode
    {
        private readonly List<CategoryNode> _children = new();

        /// <summary>
        /// Obtient le nom de la catégorie.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Obtient la couleur explicitement définie pour cette catégorie, ou null si elle doit hériter de son parent.
        /// </summary>
        public string? Color { get; private set; }

        /// <summary>
        /// Obtient le parent de cette catégorie dans l'arbre.
        /// </summary>
        public CategoryNode? Parent { get; private set; }

        /// <summary>
        /// Obtient la liste en lecture seule des enfants de cette catégorie.
        /// </summary>
        public IReadOnlyList<CategoryNode> Children => _children.AsReadOnly();

        /// <summary>
        /// Obtient la couleur effective de la catégorie, en héritant de son ascendance si aucune couleur explicite n'est définie.
        /// </summary>
        public string? EffectiveColor => Color ?? Parent?.EffectiveColor;

        /// <summary>
        /// Constructeur sans paramètre requis par Entity Framework Core pour la matérialisation.
        /// </summary>
        protected CategoryNode()
        {
            Name = null!; // EF Core se chargera d'affecter la valeur via réflexion.
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CategoryNode"/>.
        /// </summary>
        /// <param name="name">Le nom de la catégorie.</param>
        /// <param name="color">La couleur explicite au format hexadécimal (optionnelle).</param>
        public CategoryNode(string name, string? color = null)
        {
            Name = name;
            Color = color;
        }

        /// <summary>
        /// Ajoute un nœud enfant à cette catégorie et lie automatiquement ce nœud à ce parent.
        /// </summary>
        /// <param name="child">Le nœud enfant à ajouter.</param>
        public void AddChild(CategoryNode child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        /// <summary>
        /// Récupère la chaîne d'ascendance complète depuis la racine jusqu'à ce nœud inclus.
        /// </summary>
        /// <returns>Une énumération des nœuds de la racine vers la feuille.</returns>
        public IEnumerable<CategoryNode> GetAscendanceChain()
        {
            var chain = new List<CategoryNode>();
            var current = this;

            while (current != null)
            {
                chain.Add(current);
                current = current.Parent;
            }

            chain.Reverse();
            return chain;
        }
    }
}