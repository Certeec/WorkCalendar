using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace WorkCalendar.Library.Planner.ConfigurableDefaultvalues
{
    public class UserDefaultIncomeRepository : IUserDefaultIncomeRepository
    {
        private string _connectionString;
        public UserDefaultIncomeRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }
        public double GetDefaultUserIncome(int userId)
        {
            double defaultIncome = -1;
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = sqlCon;
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue(@"UserId", userId);
                com.CommandText = "SELECT MoneyPerHour FROM UserSchedulerDefaultHourIncome WHERE UserId = @UserID";
                var reader = com.ExecuteReader();

                while (reader.Read())
                {
                    defaultIncome = reader.GetDouble("MoneyPerHour");
                }
            }

            Console.WriteLine("Received income = " + defaultIncome.ToString());

            return defaultIncome;
        }

        public bool UpdateDefaultUserIncome(int userId, double moneyPerHour)
        {
            int result = 0;
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = sqlCon;
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue(@"UserId", userId);
                com.Parameters.AddWithValue(@"MoneyPerHour", moneyPerHour);
                com.CommandText = "UPDATE UserSchedulerDefaultHourIncome SET MoneyPerHour = @MoneyPerHour WHERE UserId = @UserId";
                result = com.ExecuteNonQuery();
            }
            return result == 0 ? false : true;
        }

        public bool AddDefaultUserIncome(int userId, double moneyPerHour)
        {
            int result = 0;
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.Connection = sqlCon;
                com.Parameters.AddWithValue(@"UserId", userId);
                com.Parameters.AddWithValue(@"MoneyPerHour", moneyPerHour);
                com.CommandText = "INSERT INTO UserSchedulerDefaultHourIncome (UserId, MoneyPerHour) VALUES (@UserId, @MoneyPerHour)";

                result = com.ExecuteNonQuery();
            }
            return result == 0 ? false : true;
        }
    }
}
