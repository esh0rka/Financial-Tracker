using System;
namespace Financical_Traker.Core
{
	public interface ISpending
	{
		public void ChangeInfo();
		public void ChangeStartDate();
        public void ChangeEndDate();
		public void СhangeAmountPaid();
		public void ChangeTotalAmount();
    }
}

