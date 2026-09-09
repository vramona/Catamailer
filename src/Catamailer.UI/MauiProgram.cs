// Historique :
// 2026-09-08 : Configuration du démarrage et ajout de H.NotifyIcon (J3-S1-T1).
// 2026-09-08 : Injection de IGlobalHotkeyService (J3-S1-T2).
// 2026-09-08 : Injection de QuickCategorizeViewModel (J3-S2-T1).
// 2026-09-08 : Ajout des injections pour EF Core et l'Infrastructure (J3-S2-T1).
// 2026-09-08 : Initialisation automatique du schéma SQLite au démarrage (J3-S2-T1).
// 2026-09-08 : Ajout d'un jeu de données de test (Seed) avec héritage pour validation UI (J3-S2-T1).
// 2026-09-08 : Injection de QuickRuleBuilderViewModel et d'un DummySelectionProvider (J3-S2-T2).
// 2026-09-09 : Adaptation à la refonte de MailMetadata (J3-S2-T2-ST1).
// 2026-09-09 : Injection de IRuleRepository et ClassificationEngine (J3-S2-T2-ST2).
// 2026-09-09 : Injection de ShadowModeDashboardViewModel et IShadowModeService (J3-S3-T1).

using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;
using H.NotifyIcon;
using Catamailer.Domain;
using Catamailer.Infrastructure;
using Catamailer.Application;
using Catamailer.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Catamailer.UI;

// Fournisseur factice pour valider l'IHM sans dépendre d'Outlook en phase de développement
public class DummySelectionProvider : ISelectionProvider
{
    public MailMetadata? GetSelectedMail()
    {
        return new MailMetadata(
            "dummy-id", 
            "[Urgent] Réunion Projet Alpha", 
            "client@important.com", 
            "assistant@important.com",
            new List<string>(),
            new List<string> { "team@important.com" });
    }
}

// Dépôt factice pour fournir des règles en l'absence de base de données implémentée pour DictionaryRule
public class DummyRuleRepository : IRuleRepository
{
    public Task<IEnumerable<DictionaryRule>> GetAllDictionaryRulesAsync()
    {
        var dummyRule = new DictionaryRule(
            new CategoryNode("Urgent"), 
            subjectKeywords: new[] { "Urgent" });
            
        return Task.FromResult<IEnumerable<DictionaryRule>>(new List<DictionaryRule> { dummyRule });
    }
}

/// <summary>
/// Classe statique responsable de l'amorçage et de la configuration de l'application MAUI Blazor.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Crée et configure l'instance principale de l'application MAUI.
    /// Injecte les dépendances Blazor et initialise le composant de zone de notification (Tray Icon).
    /// </summary>
    /// <returns>Une instance configurée de <see cref="MauiApp"/>.</returns>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseNotifyIcon() // Enregistrement du composant Tray Icon
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Configuration de la base de données SQLite dans le dossier local de l'application
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "catamailer.db");
        builder.Services.AddDbContext<CatamailerDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Injection des services de domaine et d'application
        builder.Services.AddTransient<ClassificationEngine>();
        builder.Services.AddScoped<IShadowModeService, ShadowModeService>(); // Ajouté pour J3-S3-T1

        // Injection des services d'infrastructure
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IRuleRepository, DummyRuleRepository>();
        builder.Services.AddScoped<IAppSettingsRepository, AppSettingsRepository>(); // Nécessaire pour ShadowModeService
        builder.Services.AddScoped<IHistoryStateRepository, HistoryStateRepository>(); // Nécessaire pour ShadowModeService
        builder.Services.AddSingleton<IGlobalHotkeyService, Win32GlobalHotkeyService>();
        
        // Faux fournisseur pour le test de l'IHM (à remplacer par OutlookSelectionProvider en prod)
        builder.Services.AddScoped<ISelectionProvider, DummySelectionProvider>();
        
        // Injection des ViewModels (Transient pour réinitialiser l'état à chaque appel)
        builder.Services.AddTransient<QuickCategorizeViewModel>();
        builder.Services.AddTransient<QuickRuleBuilderViewModel>();
        builder.Services.AddTransient<ShadowModeDashboardViewModel>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        // Initialisation du schéma de la base de données et injection des données de test
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<CatamailerDbContext>();
            dbContext.Database.EnsureCreated();

            if (!dbContext.Categories.Any())
            {
                var urgent = new CategoryNode("Urgent", "#dc3545");
                var alpha = new CategoryNode("Projet Alpha", "#0d6efd");
                var alphaDesign = new CategoryNode("Projet Alpha - Design"); // Sans couleur
                
                alpha.AddChild(alphaDesign); // Héritage de la couleur de "Projet Alpha"

                var beta = new CategoryNode("Projet Beta", "#198754");
                var aLire = new CategoryNode("A Lire", "#ffc107");

                dbContext.Categories.AddRange(urgent, alpha, alphaDesign, beta, aLire);
                dbContext.SaveChanges();
            }
        }

        return app;
    }
}