using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker_GUI.GUI;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    void ContentPage_NavigatedFrom(System.Object sender, Microsoft.Maui.Controls.NavigatedFromEventArgs e)
    {
    }

    public void RegisterClick(System.Object sender, System.EventArgs e)
    {
        RegisterClickAsync();

    }

    async void RegisterClickAsync()
    {
        if (await AccountEntry.AccountCheckAsync(mailEntry.Text))
        {
            errorMessage.Text = "Пользователь уже зарегистрирован!";
            return;
        }

        errorMessage.Text = "";
        AccountEntry.RegisterAccountAsync(mailEntry.Text, passwordEntry.Text);

        await Shell.Current.GoToAsync("FirstAccountSettingsPage");
    }

    async void LoginClick(System.Object sender, System.EventArgs e)
    {
        if (await AccountEntry.isEntered(mailEntry.Text, passwordEntry.Text))
        {
            await Shell.Current.GoToAsync("MainViewPage");
        }
        else
        {
            errorMessage.Text = "Такого пользователя не существует!";
        }
    }
}
