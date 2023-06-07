using Financical_Traker.Core.CategoryManagement;
using Financical_Traker_GUI.Models;

namespace Financical_Traker_GUI.GUI;

public partial class LeftView : ContentView
{
    private LeftViewModel _viewModel;

    public LeftView()
    {
        InitializeComponent();

        _viewModel = new LeftViewModel();
        BindingContext = _viewModel;
    }

    void ShowPage1(System.Object sender, System.EventArgs e)
    {
        cactegoryPage.IsVisible = cactegoryPage.IsVisible ? false : true;
    }

    async void ListView_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
    {
        await _viewModel.ChangeCategory();
    }

}
