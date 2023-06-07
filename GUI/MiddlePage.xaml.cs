using Financical_Traker_GUI.Models;

namespace Financical_Traker_GUI.GUI;

public partial class MiddlePage : ContentView
{
    MiddlePageModel pageModel;

    public MiddlePage()
    {
        InitializeComponent();
        pageModel = new MiddlePageModel();
        BindingContext = pageModel;
        LeftViewModel.middlePageModel = pageModel;
        pageModel.UpdateCategoriesAsync();
    }

    void GoToSettings(System.Object sender, System.EventArgs e)
    {
        GoToSettingsAsync();
    }

    async void GoToSettingsAsync()
    {
        await Shell.Current.GoToAsync("MainViewPage");
    }

    void DatePicker_DateSelected(System.Object sender, Microsoft.Maui.Controls.DateChangedEventArgs e)
    {
    }
}
