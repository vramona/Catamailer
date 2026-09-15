// Historique :
// 2026-09-08 : Nettoyage total de la page. Le TrayIcon est désormais géré globalement dans App (J3-S1-T1).

using Microsoft.Maui.Controls;

namespace Catamailer.UI;

/// <summary>
/// Page principale hébergeant exclusivement la vue Blazor.
/// </summary>
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
}