using System;
using Microsoft.EntityFrameworkCore;
using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker.Core.SpendingMenagement
{
    public class ExpenseDbContext : DbContext
    {
        private readonly string accountHash = CurrentUserData.CurrentUser.Hash;
        public DbSet<OneTimeExpense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@$"Data Source=/Users/egorgajkevic/Documents/FPDebug/expense_{accountHash}.db;");
        }
    }
}

