namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public interface IUserDefaultIncomeService
    {
        double GetUserDefaultIncome(int userId);
        bool SetUserDefaultIncome(int userId, double userIncomePerHour);
    }
}
