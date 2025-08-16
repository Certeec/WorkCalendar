using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using System.Data;
using System.Text;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Accounts
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly string _connectionString;

        public AccountsRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public bool CreateUser(CreateAccountDTO accData)
        {
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.Connection = sqlCon;
                com.Parameters.AddWithValue("@Login", accData.Login);
                com.Parameters.AddWithValue("@Password", accData.Password);
                com.Parameters.AddWithValue("@Email", accData.Email);
                com.Parameters.AddWithValue("@CreateDate", DateTime.Now);
                com.Parameters.AddWithValue("@Status", 1); // 1 Represents not activated account 
                com.CommandText = @"INSERT INTO Accounts (Login, Password, Status, Email, CreateDate) VALUES (@Login, @Password, @Status, @Email, @CreateDate)"; 
                com.ExecuteNonQuery();
            }

            return true;
        }

        public int? GetUserID(LoginCredentialsDTO credentials)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                    sqlCommand.Connection = con;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = @"SELECT UserId, LOGIN FROM ACCOUNTS WHERE Login = @login";
                sqlCommand.Parameters.AddWithValue("@Login", credentials.Login);
                //sqlCommand.Parameters.AddWithValue("@Password", credentials.Password);
                var sqlData = sqlCommand.ExecuteReader();

                while (sqlData.Read())
                {
                    return  sqlData.GetInt32("UserId");
                }

                return null;
            }
        }

        public UserAccount GetUser(LoginCredentialsDTO credentials)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                var sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = @"SELECT UserId, Login FROM ACCOUNTS WHERE Login = @login AND Password = @Password";
                sqlCommand.Parameters.AddWithValue("@Login", credentials.Login);
                sqlCommand.Parameters.AddWithValue("@Password", credentials.Password);
                var sqlData = sqlCommand.ExecuteReader();
                UserAccount userAccount = new UserAccount();

                while (sqlData.Read())
                {

                    userAccount.UserId = sqlData.GetInt32("UserId");
                    userAccount.Login = sqlData.GetString("Login");
                    //user.Password = sqlData.GetString("");
                    //userAccount.Email = sqlData.GetString("Email");

                }

                return userAccount;
            }
        }

    }
}

