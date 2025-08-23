using DAL;
using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public class UserDefaultIncomeService : IUserDefaultIncomeService
    {
        private DatabaseContext _context;

        public UserDefaultIncomeService(DatabaseContext context) 
        {
            _context = context;
        }

        public bool SetUserDefaultIncome(int userId, double userIncomePerHour)
        {
            var userCurrentSetting = GetUserDefaultIncome(userId);

            if(userCurrentSetting == null)
                return AddDefaultUserIncome(userId, userIncomePerHour);
            else
            {
                userCurrentSetting.MoneyPerHour = userIncomePerHour;
                _context.Update(userCurrentSetting);
                return _context.SaveChanges() == 1;
            }
        }

        private bool AddDefaultUserIncome(int userId, double moneyPerHour)
        {
            var userIncome = new UserSchedulerDefaultHourIncome() { MoneyPerHour = moneyPerHour, UserId = userId };
            _context.DefaultIncomes.Add(userIncome);
            return _context.SaveChanges() == 1;
        }

        public UserSchedulerDefaultHourIncome? GetUserDefaultIncome(int userId)
            => _context.DefaultIncomes.FirstOrDefault(n => n.UserId == userId);
    }
}
