// Historique :
// 2026-09-07 : Création du DbContext pour SQLite (J1-S1-T3).
// 2026-09-07 : Ajout des entités SystemState et AppSetting (J2-S2-T1).
// 2026-09-22 : Ajout du filtre de requête global (Global Query Filter) pour IsDeleted sur CategoryNode (J4-S4-T6).
// 2026-09-24 : Ajout de ExecutionRule et configuration JSON pour IReadOnlyList<RuleAction> (J5-S3-T1 - Phase Verte).

using Catamailer.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

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
        /// Obtient ou définit la collection des règles d'exécution.
        /// </summary>
        public DbSet<ExecutionRule> ExecutionRules { get; set; } = null!;

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

                // Filtre global : exclure les catégories supprimées logiquement par défaut
                entity.HasQueryFilter(e => !e.IsDeleted);
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

            // Configuration de l'entité ExecutionRule
            modelBuilder.Entity<ExecutionRule>(entity =>
            {
                entity.HasKey(e => e.Name);

                // Configuration de la racine de l'arbre des conditions en JSON
                entity.Property(e => e.RootNode)
                      .HasConversion(
                          v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                          v => JsonSerializer.Deserialize<RuleNode>(v, (JsonSerializerOptions?)null)!);

                // Configuration de la liste d'actions séquentielles en JSON
                entity.Property(e => e.Actions)
                      .HasConversion(
                          v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                          v => JsonSerializer.Deserialize<IReadOnlyList<RuleAction>>(v, (JsonSerializerOptions?)null)!);
            });
        }
    }
}