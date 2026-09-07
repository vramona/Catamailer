// Historique :
// 2026-09-07 : Qualification explicite de Application pour résoudre le conflit avec Catamailer.Application (J1-S2-T1).

namespace Catamailer.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
	public App()
	{
		InitializeComponent();
	}

	protected override Window CreateWindow(IActivationState? activationState)
	{
		return new Window(new MainPage()) { Title = "Catamailer.UI" };
	}
}