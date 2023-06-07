using System.Reflection.PortableExecutable;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker.Core.CategoryManagement
{
	public static class CategoryService
    {
        public static async Task NewCategoryAsync(string categoryName)
        {
            using var categoryContext = new CategoryDbContext();
            categoryContext.Database.EnsureCreated();
            if (await categoryContext.Categories.AnyAsync(c => c.Name == categoryName))
            {
                return;
            }
            else
            {
                var category = new Category { Name = categoryName };
                categoryContext.Add(category);
                await categoryContext.SaveChangesAsync();
            }
        }

        public static async Task<List<Category>> GetCategoriesListAsync()
        {
            using var categoryContext = new CategoryDbContext();
            categoryContext.Database.EnsureCreated();
            var categories = await categoryContext.Categories.ToListAsync();

            return categories;
        }

        public static async Task<bool> IsNameExistsAsync(string categoryName)
        {
            bool categoryExists;

            using (var categoryContext = new CategoryDbContext())
            {
                categoryExists = await categoryContext.Categories.AnyAsync(c => c.Name == categoryName);
            }

            return categoryExists;
        }

        public static async Task<string> GetNameById(int categoryId)
        {
            using (var categoryContext = new CategoryDbContext())
            {
                var category = await categoryContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);

                if (category != null)
                {
                    return category.Name;
                }
                else
                {
                    return "None";
                }
            }
        }

        public static async Task DeleteByNameAsync(string categoryName)
        {
            using var categoryContext = new CategoryDbContext();
            var categoryToDelete = await categoryContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
            if (categoryToDelete != null)
            {
                categoryContext.Categories.Remove(categoryToDelete);
                await categoryContext.SaveChangesAsync();
            }
        }

        public static async Task UpdateCategoryAsync(string categoryName, string? newCategoryName = null)
        {
            using var categoryContext = new CategoryDbContext();
            var categoryToUpdate = await categoryContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);

            if (categoryToUpdate != null)
            {
                if (newCategoryName != null)
                {
                    categoryToUpdate.Name = newCategoryName;
                }

                await categoryContext.SaveChangesAsync();
            }
        }

        public static async Task<int> GetIdFromName(string categoryName)
        {
            using var categoryContext = new CategoryDbContext();
            var category = await categoryContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);

            if (category != null)
            {
                return category.Id;
            }
            else
            {
                return -1;
            }
        }
    }
}
