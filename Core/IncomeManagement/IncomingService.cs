using System;
using Microsoft.EntityFrameworkCore;
using Financical_Traker.Core.InternalStatus;
using Financical_Traker.Core.CategoryManagement;

namespace Financical_Traker.Core.IncomeManagement
{
    public static class IncomingService
    {
        public static async Task NewIncomeAsync(string data, decimal amount, DateTime expenseDate, int categoryId)
        {
            using var incomeContext = new IncomeDbContext();
            incomeContext.Database.EnsureCreated();
            var income = new OneTimeIncome { Data = data, Amount = amount, IncomeDate = expenseDate, CategoryId = categoryId,
            Text = amount.ToString() + " " + CurrentUserData.CurrentUser.DefaultCurrency + " " + await CategoryService.GetNameById(categoryId) + " " + expenseDate.ToString() + "\n" + data
            };
            incomeContext.Add(income);
            await incomeContext.SaveChangesAsync();
        }

        public static async Task<List<OneTimeIncome>> GetIncomeListAsync()
        {
            using var incomeContext = new IncomeDbContext();
            incomeContext.Database.EnsureCreated();
            var incomes = await incomeContext.Incomes.ToListAsync();

            return incomes;
        }

        public static async Task DeleteByIdAsync(int id)
        {
            using var incomeContext = new IncomeDbContext();
            var incomeToDelete = await incomeContext.Incomes.FirstOrDefaultAsync(c => c.Id == id);
            if (incomeToDelete != null)
            {
                incomeContext.Incomes.Remove(incomeToDelete);
                await incomeContext.SaveChangesAsync();
            }
        }
    }
}
