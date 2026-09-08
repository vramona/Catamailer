// Historique :
// 2026-09-08 : Implémentation du démarrage masqué (Headless) (J3-S1-T1).
// 2026-09-08 : Refonte totale du TrayIcon vers App.xaml via MVVM (Commandes) pour résoudre la perte d'événements (J3-S1-T1).
// 2026-09-08 : Interception de l'événement natif Closing pour masquer la fenêtre au lieu de tuer l'application (J3-S1-T1).

using System;
using System.Windows.Input;
using H.NotifyIcon;
using Microsoft.Maui.Controls;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace Catamailer.UI;

/// <summary>
/// Représente l'application principale MAUI.
/// Gère le cycle de vie de la fenêtre, le démarrage en mode furtif (Headless) et les actions globales (Tray Icon).
/// </summary>
public partial class App : Microsoft.Maui.Controls.Application
{
    private Microsoft.Maui.Controls.Window? _mainWindow;
    private TaskbarIcon? _trayIcon;

    /// <summary>
    /// Commande liée à l'action "Ouvrir le Dashboard" du menu.
    /// </summary>
    public ICommand OpenCommand { get; }

    /// <summary>
    /// Commande liée à l'action "Quitter" du menu.
    /// </summary>
    public ICommand ExitCommand { get; }

    /// <summary>
    /// Initialise une nouvelle instance de la classe <see cref="App"/>.
    /// </summary>
    public App()
    {
        // Initialisation des commandes MVVM avant InitializeComponent pour le Binding
        OpenCommand = new Command(ExecuteOpen);
        ExitCommand = new Command(ExecuteExit);

        InitializeComponent();

        // Récupération de l'icône depuis les ressources globales et application du contexte de Binding
        if (Resources.TryGetValue("GlobalTrayIcon", out var resource) && resource is TaskbarIcon icon)
        {
            _trayIcon = icon;
            _trayIcon.BindingContext = this;
            _trayIcon.ForceCreate();
        }
    }

    /// <summary>
    /// Surcharge la création de la fenêtre principale.
    /// Intercepte la création sous Windows pour la masquer instantanément et modifier son comportement de fermeture.
    /// </summary>
    protected override Microsoft.Maui.Controls.Window CreateWindow(Microsoft.Maui.IActivationState? activationState)
    {
        _mainWindow = new Microsoft.Maui.Controls.Window(new MainPage());

        _mainWindow.Created += (s, e) =>
        {
#if WINDOWS
            var nativeWindow = _mainWindow.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
            if (nativeWindow != null)
            {
                var windowHandle = WindowNative.GetWindowHandle(nativeWindow);
                var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
                var appWindow = AppWindow.GetFromWindowId(windowId);
                
                // Interception de la fermeture pour masquer la fenêtre au lieu de la détruire
                appWindow.Closing += (sender, args) =>
                {
                    args.Cancel = true; // Empêche la destruction de la fenêtre
                    appWindow.Hide();   // Masque la fenêtre (retourne en TrayIcon)
                };

                // Utilisation du Dispatcher pour masquer la fenêtre APRÈS que le framework MAUI l'ait forcée à s'afficher au démarrage
                Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
                {
                    appWindow.Hide();
                });
            }
#endif
        };

        return _mainWindow;
    }

    /// <summary>
    /// Logique d'affichage de la fenêtre principale.
    /// </summary>
    private void ExecuteOpen()
    {
#if WINDOWS
        if (_mainWindow?.Handler?.PlatformView is Microsoft.UI.Xaml.Window nativeWindow)
        {
            var windowHandle = WindowNative.GetWindowHandle(nativeWindow);
            var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            
            appWindow.Show();
            nativeWindow.Activate();
        }
#endif
    }

    /// <summary>
    /// Logique de terminaison du processus.
    /// </summary>
    private void ExecuteExit()
    {
        try
        {
            _trayIcon?.Dispose();
        }
        catch
        {
            // Ignorer l'erreur au moment du kill process
        }
        
        Environment.Exit(0);
    }
}