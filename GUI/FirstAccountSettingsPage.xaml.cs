using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker_GUI.GUI;

public partial class FirstAccountSettingsPage : ContentPage
{
    public FirstAccountSettingsPage()
    {
        InitializeComponent();
    }

    void EndRegistration(System.Object sender, System.EventArgs e)
    {
        Task task = EndRegistrationAsync();
    }

    async Task EndRegistrationAsync()
    {
        errorMessage.Text = "";
        startButton.IsEnabled = false;
        await AccountEntry.ContinueAccountRegisterAsync(CurrentUserData.CurrentUser.Mail, name.Text, default_currency.SelectedItem as string, timezone.SelectedItem as string);
        await Shell.Current.GoToAsync("MainViewPage");
    }
}
