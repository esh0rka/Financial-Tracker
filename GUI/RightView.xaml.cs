using Financical_Traker.Core.SpendingMenagement;
using Financical_Traker_GUI.Models;

namespace Financical_Traker_GUI.GUI;

public partial class RightView : ContentView
{
    private RightViewModel _viewModel;

    public RightView()
    {
        InitializeComponent();

        _viewModel = new RightViewModel();
        BindingContext = _viewModel;
        MiddlePageModel.rightViewModel = _viewModel;
    }

    void ShowPage1(System.Object sender, System.EventArgs e)
    {
        expensePage.IsVisible = expensePage.IsVisible ? false : true;
    }

    void ListView_ItemSelected(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
    {
    }

    void ShowPage2(System.Object sender, System.EventArgs e)
    {
        incomesPage.IsVisible = incomesPage.IsVisible ? false : true;
    }
}
