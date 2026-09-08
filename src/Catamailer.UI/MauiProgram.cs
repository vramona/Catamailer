// Historique :
// 2026-09-08 : Configuration du démarrage et ajout de H.NotifyIcon (J3-S1-T1).

using Microsoft.Extensions.Logging;
using H.NotifyIcon; // CORRECTION : L'espace de noms ne prend pas le suffixe .Maui

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

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}