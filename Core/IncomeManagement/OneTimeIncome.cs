using System;

namespace Financical_Traker.Core.IncomeManagement
{
    public class OneTimeIncome
    {
        public OneTimeIncome()
        {
        }

        public int Id { get; set; }
        public string Data { get; set; }
        public decimal Amount { get; set; }
        public DateTime IncomeDate { get; set; }
        public int CategoryId { get; set; }
        public string Text { get; set; }
    }
}

