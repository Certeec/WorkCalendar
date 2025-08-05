namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public interface IUserDefaultIncomeRepository
    {
        double GetDefaultUserIncome(int userId);
        bool UpdateDefaultUserIncome(int userId, double moneyPerHour);
        bool AddDefaultUserIncome(int userId, double moneyPerHour);
    }
}
