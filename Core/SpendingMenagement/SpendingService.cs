using System;
/*
namespace Financical_Traker.Core
{
	public class SpendingService
	{
		public SpendingService()
		{
		}

		public static void NewInterestFreeDebt(string newInfo, DateTime newStartDate, DateTime newEndDate, decimal newAmountPaid, decimal newTotalAmount, bool isCycle, TimeSpan newCycleTime, decimal newOneCyclePaid)
		{
			InterestFreeDebt newDebt = new(newInfo, newStartDate, newEndDate, newAmountPaid, newTotalAmount, isCycle, newCycleTime, newOneCyclePaid);
			CurrentUserData.InterestFreeDebtList.Add(newDebt);
		}

		public static void AllInterestFreeDebtCheck()
		{
			DateTime currentDate = DateTime.Now;

			foreach (InterestFreeDebt debt in CurrentUserData.InterestFreeDebtList)
			{
				debt.ChangeLastUpdate(currentDate);
				if (debt.IsCycle)
				{
                    TimeSpan timeDifference = currentDate - debt.LastUpdate;
					int cycleTimes = (int)(timeDifference.Ticks / debt.CycleTime.Ticks);
					if (cycleTimes > 0)
					{
						debt.AddPaid(debt.OneCyclePaid * cycleTimes);
					}
                }
			}
		}
	}
}
*/
