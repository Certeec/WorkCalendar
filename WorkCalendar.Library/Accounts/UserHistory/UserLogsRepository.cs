using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
namespace WorkCalendar.Library.Accounts.UserHistory
{
    public class UserLogsRepository : IUserLogsRepository
    {
        private string _connectionString;

        public UserLogsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public List<UserLogInHistory> ReadUserLogs(int UserId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = @"SELECT UserId, LoginDate, Duration, UserIp FROM UserLoginLogs WHERE UserId = @UserId";
                sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                var sqlData = sqlCommand.ExecuteReader();


                List<UserLogInHistory> userLogs = new List<UserLogInHistory>();

                while (sqlData.Read())
                {
                    UserLogInHistory history = new UserLogInHistory();
                    history.UserId = sqlData.GetInt32("UserId");
                    history.date = sqlData.GetDateTime("LoginDate");
                    history.duration = sqlData.GetDateTime("Duration");
                    history.ip = sqlData.GetString("UserIp");
                    userLogs.Add(history);
                }

                return userLogs;
            }
        }

        public void RegisterLoginIn(int userId)
        {
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = sqlCon;
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue("@UserId", userId);
                com.Parameters.AddWithValue("@LoginDate", DateTime.Now);
                com.Parameters.AddWithValue("@Duration", 5);
                com.Parameters.AddWithValue("@UserIp", "100.128.0.1");
                com.CommandText = @"INSERT INTO UserLoginLogs (UserId, LoginDate, Duration, UserIp) VALUES (@UserID, @LoginDate, @Duration, @UserIp)";
                com.ExecuteNonQuery();
            }
        }
    }
}
