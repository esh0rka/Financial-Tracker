using System;
using Microsoft.EntityFrameworkCore;
using Financical_Traker.Core.InternalStatus;

namespace Financical_Traker.Core.CategoryManagement
{
	public class CategoryDbContext : DbContext
	{
        private readonly string accountHash = CurrentUserData.CurrentUser.Hash;
		public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite(@$"Data Source=categories_{accountHash}.db;");
            optionsBuilder.UseSqlite(@$"Data Source=/Users/egorgajkevic/Documents/FPDebug/categories_{accountHash}.db;");
        }
    }
}
