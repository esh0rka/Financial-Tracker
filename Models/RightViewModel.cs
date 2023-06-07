using System;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Financical_Traker.Core.SpendingMenagement;
using System;
using Financical_Traker.Core.CategoryManagement;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Financical_Traker.Core.IncomeManagement;

namespace Financical_Traker_GUI.Models
{
	public partial class RightViewModel : ObservableObject
    {
		public RightViewModel()
		{
            Expenses = new ObservableCollection<OneTimeExpense>();
            GetExpensesList();
        }

        [ObservableProperty]
        public Category selectedExpense;

        [ObservableProperty]
        public ObservableCollection<OneTimeExpense> expenses;
        
        public async void GetExpensesList()
        {
            Expenses = new(await OneTimeExpenseServiece.GetExpenseListAsync());
        }

        public async Task ChangeExpense()
        {
            
        }

        [ObservableProperty]
        public Category selectedIncome;

        [ObservableProperty]
        public ObservableCollection<OneTimeIncome> incomes;

        public async void GetIncomesList()
        {
            Incomes = new(await IncomingService.GetIncomeListAsync());
        }
    }
}

