using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public class UserDefaultIncomeService : IUserDefaultIncomeService
    {
        private IUserDefaultIncomeRepository _userDefaultIncomeRepository;

        public UserDefaultIncomeService(IUserDefaultIncomeRepository userDefaultIncomeRepository) 
        {
            _userDefaultIncomeRepository = userDefaultIncomeRepository;
        }

        public double GetUserDefaultIncome(int userId)
        {
            var returnedValue = _userDefaultIncomeRepository.GetDefaultUserIncome(userId);

            return returnedValue == -1 ? 0 : returnedValue; 
        }

        public bool SetUserDefaultIncome(int userId, double userIncomePerHour)
        {
            var userCurrentSetting = _userDefaultIncomeRepository.GetDefaultUserIncome(userId);

            if(userCurrentSetting == -1)
            {
                return _userDefaultIncomeRepository.AddDefaultUserIncome(userId, userIncomePerHour);
            }
            else
            {
                return _userDefaultIncomeRepository.UpdateDefaultUserIncome(userId,userIncomePerHour);
            }
        }
    }
}
