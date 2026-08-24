using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public interface IUserDefaultIncomeService
    {
        UserSchedulerDefaultHourIncome? GetUserDefaultIncome(int userId);
        bool SetUserDefaultIncome(int userId, double userIncomePerHour);
    }
}
