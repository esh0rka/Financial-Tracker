using System;
using Microsoft.EntityFrameworkCore;
using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker.Core.IncomeManagement
{
    public class IncomeDbContext : DbContext
    {
        private readonly string accountHash = CurrentUserData.CurrentUser.Hash;
        public DbSet<OneTimeIncome> Incomes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@$"Data Source=income_{accountHash}.db;");
            optionsBuilder.UseSqlite(@$"Data Source=/Users/egorgajkevic/Documents/FPDebug/income_{accountHash}.db;");
        }
    }
}

