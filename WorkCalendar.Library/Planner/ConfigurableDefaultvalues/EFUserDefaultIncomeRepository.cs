using DAL;
using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public class EFUserDefaultIncomeRepository : IUserDefaultIncomeRepository
    {
        private DatabaseContext _context;

        public EFUserDefaultIncomeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public bool AddDefaultUserIncome(int userId, double moneyPerHour)
        {
            var userIncome = new UserSchedulerDefaultHourIncome() { MoneyPerHour = moneyPerHour, UserId = userId };
            _context.DefaultIncomes.Add(userIncome);
            return _context.SaveChanges() == 1;
        }

        public double GetDefaultUserIncome(int userId)
        {
            var result = _context.Set<UserSchedulerDefaultHourIncome>().FirstOrDefault(n => n.UserId == userId);
            return result.MoneyPerHour;
        }

        public bool UpdateDefaultUserIncome(int userId, double moneyPerHour)
        {
            var result = _context.DefaultIncomes.FirstOrDefault(n => n.UserId == userId);
            result.MoneyPerHour = moneyPerHour;

            _context.DefaultIncomes.Update(result);
            return _context.SaveChanges() == 1;
        }
    }
}
