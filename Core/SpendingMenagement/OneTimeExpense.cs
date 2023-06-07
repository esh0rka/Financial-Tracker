using System;
namespace Financical_Traker.Core.SpendingMenagement
{
	public class OneTimeExpense
	{
		public OneTimeExpense()
		{
		}

		public int Id { get; set; }
        public string Data { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
		public int CategoryId { get; set; }
		public string Text { get; set; }
	}
}

