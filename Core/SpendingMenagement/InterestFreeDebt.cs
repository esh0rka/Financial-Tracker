using System;



namespace Financical_Traker.Core
{
	public class InterestFreeDebt : ISpending
	{
		private DateTime lastUpdate;
		private string info;
		private DateTime startDate;
		private DateTime endDate;
		private decimal amountPaid;
		private decimal totalAmount;
		private decimal difference;
        bool cycle;
        TimeSpan cycleTime;
        private decimal oneCyclePaid;

        public InterestFreeDebt(string newInfo, DateTime newStartDate, DateTime newEndDate, decimal newAmountPaid, decimal newTotalAmount, bool isCycle, TimeSpan newCycleTime, decimal newOneCyclePaid)
		{
			lastUpdate = DateTime.Now;
			info = newInfo;
			startDate = newStartDate;
			endDate = newEndDate;
			amountPaid = newAmountPaid;
			totalAmount = newTotalAmount;
			difference = totalAmount - amountPaid;
            cycle = isCycle;
            cycleTime = newCycleTime;
            oneCyclePaid = newOneCyclePaid;
        }

        public DateTime LastUpdate
        {
            get { return lastUpdate; }
        }

        public string Info
		{
			get { return info; }
		}

        public DateTime StartDate
        {
            get { return startDate; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
        }

        public decimal AmountPaid
        {
            get { return amountPaid; }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
        }

        public decimal Difference
        {
            get { return difference; }
        }

        public bool IsCycle
        {
            get { return cycle; }
        }

        public TimeSpan CycleTime
        {
            get { return cycleTime; }
        }

        public decimal OneCyclePaid
        {
            get { return oneCyclePaid; }
        }

        public void ChangeInfo(string newInfo)
        {
            info = newInfo;
        }

        public void ChangeStartEndDate(DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate > newEndDate)
            {
                throw new InvalidOperationException("Start Date > End Date");
            }

            startDate = newStartDate;
            endDate = newEndDate;
        }

        public void ChangeAmountPaid(decimal newAmountPaid)
        {
            amountPaid = newAmountPaid;
            difference = totalAmount - amountPaid;
        }

        public void AddPaid(decimal amount)
        {
            amountPaid += amount;
            difference = totalAmount - amountPaid;
        }

        public void ChangeTotalAmount(decimal newTotalAmount)
        {
            totalAmount = newTotalAmount;
            difference = totalAmount - amountPaid;
        }

        public void ChangeCycleStatus(bool newCycleStatus)
        {
            cycle = newCycleStatus;
        }

        public void ChangeCycleTime(TimeSpan newCycleTime)
        {
            cycleTime = newCycleTime;
        }

        public void ChangeLastUpdate(DateTime newLastUpdate)
        {
            lastUpdate = newLastUpdate;
        }

        public void ChangeOneCyclePaid(decimal newOneCyclePaid)
        {
            oneCyclePaid = newOneCyclePaid;
        }

        public void ChangeInfo()
        {
            throw new NotImplementedException();
        }

        public void ChangeStartDate()
        {
            throw new NotImplementedException();
        }

        public void ChangeEndDate()
        {
            throw new NotImplementedException();
        }

        public void СhangeAmountPaid()
        {
            throw new NotImplementedException();
        }

        public void ChangeTotalAmount()
        {
            throw new NotImplementedException();
        }
    }
}
