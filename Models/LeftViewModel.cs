using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Financical_Traker.Core.CategoryManagement;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace Financical_Traker_GUI.Models
{
	public partial class LeftViewModel : ObservableObject
    {
        public static MiddlePageModel middlePageModel;

        public LeftViewModel()
		{
            Categories = new ObservableCollection<Category>();
            GetCategorisList();
		}

		[ObservableProperty]
		public ObservableCollection<Category> categories;

        [ObservableProperty]
        public Category selectedCategory;

        public async void GetCategorisList()
		{
            Categories = new(await CategoryService.GetCategoriesListAsync());
        }

        [RelayCommand]
        public async Task AddCategory(string categoryName)
        {
            await CategoryService.NewCategoryAsync(categoryName);
            middlePageModel.UpdateCategoriesAsync();
            Categories = new(await CategoryService.GetCategoriesListAsync());
        }

        public async Task ChangeCategory()
        {
            string selectedAction = await Application.Current.MainPage.DisplayActionSheet(null, "Отмена", null, "Изменить название категории", "Удалить категорию");

            if (selectedAction == "Изменить название категории")
            {
                string userInput = await Application.Current.MainPage.DisplayPromptAsync(SelectedCategory.Name, "Введите новое название категории", accept: "ОК", cancel: "Отмена", placeholder: "Значение");

                if (userInput != null)
                {
                    await CategoryService.UpdateCategoryAsync(SelectedCategory.Name, userInput);
                    middlePageModel.UpdateCategoriesAsync();
                    GetCategorisList();
                }
            }
            else
            {
                await CategoryService.DeleteByNameAsync(SelectedCategory.Name);
                middlePageModel.UpdateCategoriesAsync();
                GetCategorisList();
            }

            
        }
    }
}

