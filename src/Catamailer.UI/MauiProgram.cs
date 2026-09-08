// Historique :
// 2026-09-08 : Configuration du démarrage et ajout de H.NotifyIcon (J3-S1-T1).
// 2026-09-08 : Injection de IGlobalHotkeyService (J3-S1-T2).

using Microsoft.Extensions.Logging;
using H.NotifyIcon;
using Catamailer.Domain;
using Catamailer.Infrastructure;

namespace Catamailer.UI;

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

        // Injection des services d'infrastructure
        builder.Services.AddSingleton<IGlobalHotkeyService, Win32GlobalHotkeyService>();

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}