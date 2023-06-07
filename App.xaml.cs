using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Financical_Traker_GUI.GUI;
using Financical_Traker.Core.InternalStatus;
using UserDataSerialization;
using Financical_Traker.Core.AccountManagement;

namespace Financical_Traker_GUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("FirstAccountSettingsPage", typeof(GUI.FirstAccountSettingsPage));
            Routing.RegisterRoute("LoginPage", typeof(GUI.LoginPage));
            Routing.RegisterRoute("MainViewPage", typeof(GUI.MainViewPage));
            Routing.RegisterRoute("SettingsPage", typeof(GUI.SettingsPage));

            // var userData = new DataSerializer<User>("userData.json");
            var userData = new DataSerializer<User>("/Users/egorgajkevic/Documents/FPDebug/userData.json");
            if (userData.CheckIfFileExists())
            {
                CurrentUserData.CurrentUser = userData.Deserialize();
                System.Console.WriteLine(userData.Deserialize().UTC_value + "lll");
                CurrentUserData.isUserEntered = true;
            }
            else
            {
                CurrentUserData.isUserEntered = false;
            }

            if (!CurrentUserData.isUserEntered)
            {
                MainPage = new AppShell();
            }
            else
            {
                if (CurrentUserData.CurrentUser.Name == "")
                {
                    //MainPage = new AppShell();
                    MainPage = new FirstAccountSettingsPage();
                }
                else
                {
                    //MainPage = new AppShell();
                    MainPage = new MainViewPage();
                }
            }
        }
    }
}
