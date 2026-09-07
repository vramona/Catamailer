// Historique :
// 2026-09-07 : Création du DbContext pour SQLite (J1-S1-T3).
// 2026-09-07 : Ajout des entités SystemState et AppSetting (J2-S2-T1).

using Catamailer.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catamailer.Infrastructure
{
    /// <summary>
    /// Contexte de base de données principal pour Catamailer (Entity Framework Core SQLite).
    /// </summary>
    public class CatamailerDbContext : DbContext
    {
        /// <summary>
        /// Obtient ou définit la collection des nœuds de catégories.
        /// </summary>
        public DbSet<CategoryNode> Categories { get; set; } = null!;

        /// <summary>
        /// Obtient ou définit la collection des états systèmes.
        /// </summary>
        public DbSet<SystemState> SystemStates { get; set; } = null!;

        /// <summary>
        /// Obtient ou définit la collection des paramètres d'application.
        /// </summary>
        public DbSet<AppSetting> AppSettings { get; set; } = null!;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="CatamailerDbContext"/>.
        /// </summary>
        /// <param name="options">Les options de configuration du contexte.</param>
        public CatamailerDbContext(DbContextOptions<CatamailerDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Configure le modèle de la base de données via la Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Le constructeur de modèle.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de l'entité CategoryNode
            modelBuilder.Entity<CategoryNode>(entity =>
            {
                // Utilisation du nom comme clé primaire (MDM - Référentiel Absolu)
                entity.HasKey(e => e.Name);
                
                // Configuration de l'arbre auto-référencé
                entity.HasOne(e => e.Parent)
                      .WithMany(e => e.Children)
                      .HasForeignKey("ParentName")
                      .OnDelete(DeleteBehavior.ClientSetNull);
                      
                // Indiquer à EF Core d'utiliser le champ privé _children pour la collection
                var childrenNavigation = entity.Metadata.FindNavigation(nameof(CategoryNode.Children));
                if (childrenNavigation != null)
                {
                    childrenNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
                }
            });

            // Configuration de l'entité SystemState
            modelBuilder.Entity<SystemState>(entity =>
            {
                entity.HasKey(e => e.Key);
            });

            // Configuration de l'entité AppSetting
            modelBuilder.Entity<AppSetting>(entity =>
            {
                entity.HasKey(e => e.Key);
            });
        }
    }
}