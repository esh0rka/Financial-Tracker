using System;
using Microsoft.EntityFrameworkCore;
using Financical_Traker.Core.InternalStatus;
using Financical_Traker.Core.CategoryManagement;

namespace Financical_Traker.Core.SpendingMenagement
{
	public static class OneTimeExpenseServiece
	{
        public static async Task NewExpenseAsync(string data, decimal amount, DateTime expenseDate, int categoryId)
        {
            using var expenseContext = new ExpenseDbContext();
            expenseContext.Database.EnsureCreated();
            var expense = new OneTimeExpense { Data = data, Amount = amount, ExpenseDate = expenseDate, CategoryId = categoryId,
            Text = amount.ToString() + " " + CurrentUserData.CurrentUser.DefaultCurrency + " " + await CategoryService.GetNameById(categoryId) + " " + expenseDate.ToString() + "\n" + data};
            System.Console.WriteLine(expense.Text);
            expenseContext.Add(expense);
            await expenseContext.SaveChangesAsync();
        }

        public static async Task<List<OneTimeExpense>> GetExpenseListAsync()
        {
            using var expenseContext = new ExpenseDbContext();
            expenseContext.Database.EnsureCreated();

            var expenses = await expenseContext.Expenses.ToListAsync();

            return expenses;
        }

        public static async Task DeleteByIdAsync(int id)
        {
            using var expenseContext = new ExpenseDbContext();
            var expenseToDelete = await expenseContext.Expenses.FirstOrDefaultAsync(c => c.Id == id);
            if (expenseToDelete != null)
            {
                expenseContext.Expenses.Remove(expenseToDelete);
                await expenseContext.SaveChangesAsync();
            }
        }
    }
}

