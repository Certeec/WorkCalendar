namespace WorkCalendar.Client.Data.Scheduler.SchedulerUserDefaults
{
	public interface ISchedulerDefaultHourIncomeService
	{
		Task<double> GetUserDefaultIncome();
		Task<bool> SetUserDefaultIncome(double income);
	}
}
