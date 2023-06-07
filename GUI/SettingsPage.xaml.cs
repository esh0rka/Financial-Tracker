using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker_GUI.GUI;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    void Exit(System.Object sender, System.EventArgs e)
    {
		ExitAsync();
    }

	async void ExitAsync()
	{
        AccountEntry.UserExit();
        await Shell.Current.GoToAsync("LoginPage");
    }
}
