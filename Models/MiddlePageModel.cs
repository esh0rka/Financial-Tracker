using System;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Financical_Traker_GUI.GUI;
using Financical_Traker.Core.CategoryManagement;
using Financical_Traker.Core.SpendingMenagement;
using Financical_Traker.Core.IncomeManagement;
using System.Collections.ObjectModel;
using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker_GUI.Models
{
	public partial class MiddlePageModel : ObservableObject
    {
        public static RightViewModel rightViewModel;

        public MiddlePageModel()
		{
			UpdateStat();
			Date = DateTime.Now;
			ProfileName = CurrentUserData.CurrentUser.Name;
        }

		[ObservableProperty]
		private string statistics;

        [ObservableProperty]
        private string profileName;

        [ObservableProperty]
        private ObservableCollection<Category> categories;

		[ObservableProperty]
		private string command;

        [ObservableProperty]
        private Category categoryPick;

        [ObservableProperty]
        private DateTime date;

        public async void UpdateCategoriesAsync()
		{
            Categories = new(await CategoryService.GetCategoriesListAsync());
        }

        [ObservableProperty]
        private string? amount = "0";

        [RelayCommand]
        private void AddValue(string value)
		{
			if (Amount.Length > 7 && Amount[^1] != ',')
			{
				return;
			}
			else if (Amount == "0" && value != ",")
			{
				Amount = value;
			}
			else if (value == "," && Amount.Contains(','))
			{
				return;
			}
			else if (value == "0" && Amount == "0")
			{
				return;
			}
			else
			{
				Amount += value;
			}
		}


        [RelayCommand]
        private void Enter(string data)
		{
			

            if (Command == "Расход")
			{
				ExpenseAsync(data);
				UpdateStat();
            }
			else if (Command == "Доход")
			{
				IncomeAsync(data);
				UpdateStat();
            }	

        }

		[RelayCommand]
		private async void UserClick()
		{
            string selectedAction = await Application.Current.MainPage.DisplayActionSheet(null, "Отмена", null, "Изменить пользовательские данные", "Выйти");

			if (selectedAction == "Выйти")
			{
				AccountEntry.UserExit();
				Application.Current.MainPage = new AppShell();

            }
        }

		private async void UpdateStat()
		{
            decimal expns = 0;
            foreach (var exp in await OneTimeExpenseServiece.GetExpenseListAsync())
            {
                expns += exp.Amount;
            }

            decimal incoms = 0;
			foreach (var incs in await IncomingService.GetIncomeListAsync())
			{
				incoms += incs.Amount;
			}

			Statistics = "Потрачено: " + expns.ToString() + "\nЗаработано: " + incoms.ToString();
        }

		private async void ExpenseAsync(string data)
		{
            int categoryId = await CategoryService.GetIdFromName(CategoryPick.Name);
            //Amount = CategoryPick.Name;
            await OneTimeExpenseServiece.NewExpenseAsync(data, Decimal.Parse(Amount), Date, categoryId);
			rightViewModel.GetExpensesList();
        }

        private async void IncomeAsync(string data)
        {
            int categoryId = await CategoryService.GetIdFromName(CategoryPick.Name);
            //Amount = CategoryPick.Name;
            await IncomingService.NewIncomeAsync(data, Decimal.Parse(Amount), Date, categoryId);
            rightViewModel.GetIncomesList();
        }

        [RelayCommand]
		private void Remove()
		{
			if (Amount.Length == 1)
			{
				Amount = "0";
			}
			else
			{
				Amount = Amount[..^1];
			}
		}
	}
}
