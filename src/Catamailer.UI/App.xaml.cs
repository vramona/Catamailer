// Historique :
// 2026-09-08 : Implémentation du démarrage masqué (Headless) (J3-S1-T1).
// 2026-09-08 : Refonte totale du TrayIcon vers App.xaml via MVVM (Commandes) pour résoudre la perte d'événements (J3-S1-T1).
// 2026-09-08 : Interception de l'événement natif Closing pour masquer la fenêtre au lieu de tuer l'application (J3-S1-T1).
// 2026-09-08 : Initialisation du Hook de raccourcis Win32 sur la fenêtre native (J3-S1-T2).
// 2026-09-08 : Ajout d'un raccourci de test (Ctrl+Alt+K) pour valider l'interception fonctionnelle (J3-S1-T2).
// 2026-09-15 : Ajout des commandes de navigation vers Blazor via IBlazorNavigationService (J3-S3-T7).
// 2026-09-15 : Abonnement à l'événement ExitRequested du service de navigation (J3-S3-T7 - Phase Bleue).

using System;
using System.Windows.Input;
using H.NotifyIcon;
using Microsoft.Maui.Controls;
using Catamailer.Domain;
using Catamailer.Infrastructure;
using Catamailer.Application.Services;
using Microsoft.Extensions.DependencyInjection;
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

    public ICommand OpenCommand { get; }
    public ICommand OpenRulesCommand { get; }
    public ICommand OpenPreferencesCommand { get; }
    public ICommand ExitCommand { get; }

    public App()
    {
        OpenCommand = new Command(() => ExecuteNavigateAndOpen("/"));
        OpenRulesCommand = new Command(() => ExecuteNavigateAndOpen("/rule-builder"));
        OpenPreferencesCommand = new Command(() => ExecuteNavigateAndOpen("/preferences"));
        ExitCommand = new Command(ExecuteExit);

        InitializeComponent();

        if (Resources.TryGetValue("GlobalTrayIcon", out var resource) && resource is TaskbarIcon icon)
        {
            _trayIcon = icon;
            _trayIcon.BindingContext = this;
            _trayIcon.ForceCreate();
        }
    }

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
                
                // Abonnement au service de navigation pour intercepter les demandes de fermeture depuis Blazor
                var navService = _mainWindow.Handler?.MauiContext?.Services.GetService<IBlazorNavigationService>();
                if (navService != null)
                {
                    navService.ExitRequested += (sender, args) =>
                    {
                        Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
                        {
                            ExecuteExit();
                        });
                    };
                }

                // Initialisation du Hook Win32 pour intercepter les raccourcis globaux
                var hotkeyService = _mainWindow.Handler?.MauiContext?.Services.GetService<IGlobalHotkeyService>() as Win32GlobalHotkeyService;
                if (hotkeyService != null)
                {
                    hotkeyService.Initialize(windowHandle);

                    // Abonnement pour valider le déclenchement
                    hotkeyService.HotkeyPressed += (sender, id) =>
                    {
                        if (id == 1) // Identifiant de notre raccourci de test
                        {
                            Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
                            {
                                ExecuteNavigateAndOpen("/"); // Ouvre la fenêtre via le raccourci sur le dashboard
                            });
                        }
                    };

                    // 0x0003 = MOD_ALT (1) | MOD_CONTROL (2), 0x4B = K
                    hotkeyService.RegisterHotkey(1, 0x0003, 0x4B);
                }

                appWindow.Closing += (sender, args) =>
                {
                    args.Cancel = true; 
                    appWindow.Hide();   
                };

                Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
                {
                    appWindow.Hide();
                });
            }
#endif
        };

        return _mainWindow;
    }

    private void ExecuteNavigateAndOpen(string uri)
    {
        // 1. Déclencher la navigation côté Blazor
        var navService = _mainWindow?.Handler?.MauiContext?.Services.GetService<IBlazorNavigationService>();
        navService?.RequestNavigation(uri);

        // 2. Afficher la fenêtre Native
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

    private void ExecuteExit()
    {
        try
        {
            _trayIcon?.Dispose();
        }
        catch { }
        
        Environment.Exit(0);
    }
}