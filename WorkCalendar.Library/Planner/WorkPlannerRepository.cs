using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using System.Data;
using System.Text;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
    public class WorkPlannerRepository : IWorkPlannerRepository
    {
        private readonly string _connectionString;
        
        public WorkPlannerRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public List<SchedulerTask> GetAllSchedulerTasks(int userId)
        {
            List<SchedulerTask> tasks = new List<SchedulerTask>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = con;
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.CommandText = @"SELECT UserId, TaskId, DateStart, DateEnd, TimeLength, TaskType, Description, Place, MoneyPerHour, Bonus, IsActive, Revenue, RevenueParticipation, PremiumPoints FROM  WorkPlannerTasks WHERE UserId = @UserId";

                var sqlData = sqlCommand.ExecuteReader();



                while (sqlData.Read())
                {
                    var task = new SchedulerTask();
                    task.UserId = sqlData.GetInt32("UserId");
                    task.TaskId = sqlData.GetInt32("TaskId");
                    task.DateStart = sqlData.GetDateTime("DateStart");
                    task.DateEnd = sqlData.GetDateTime("DateEnd");
                    task.TimeLength = sqlData.GetDouble("TimeLength");
                    task.TaskType = sqlData.GetString("TaskType");
                    task.Description = sqlData.GetString("Description");
					task.Place = sqlData.GetString("Place");
					task.MoneyPerHour = sqlData.GetDouble("MoneyPerHour");
                    task.Bonus = sqlData.GetDouble("Bonus");
                    task.IsActive = sqlData.GetBoolean("IsActive");
                    task.Revenue = sqlData.GetDouble("Revenue");
                    task.RevenueParticipation = sqlData.GetDouble("RevenueParticipation");
                    task.PremiumPoints = sqlData.GetDouble("PremiumPoints");

                    tasks.Add(task);
                }
            }

            return tasks;
        }

		public SchedulerTask GetSchedulerTaskById(int userId, int taskId)
		{

			var task = new SchedulerTask();
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Connection = con;
				sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@TaskId", taskId);
				sqlCommand.CommandText = @"SELECT UserId, TaskId, DateStart, DateEnd, TimeLength, TaskType, Description, Place, MoneyPerHour, Bonus, IsActive, Revenue, RevenueParticipation, PremiumPoints FROM  WorkPlannerTasks WHERE UserId = @UserId AND TaskId = @TaskId";

				var sqlData = sqlCommand.ExecuteReader();


				while (sqlData.Read())
				{
					task.UserId = sqlData.GetInt32("UserId");
					task.TaskId = sqlData.GetInt32("TaskId");
					task.DateStart = sqlData.GetDateTime("DateStart");
					task.DateEnd = sqlData.GetDateTime("DateEnd");
					task.TimeLength = sqlData.GetDouble("TimeLength");
					task.TaskType = sqlData.GetString("TaskType");
					task.Description = sqlData.GetString("Description");
					task.Place = sqlData.GetString("Place");
					task.MoneyPerHour = sqlData.GetDouble("MoneyPerHour");
					task.Bonus = sqlData.GetDouble("Bonus");
					task.IsActive = sqlData.GetBoolean("IsActive");
					task.Revenue = sqlData.GetDouble("Revenue");
					task.RevenueParticipation = sqlData.GetDouble("RevenueParticipation");
					task.PremiumPoints = sqlData.GetDouble("PremiumPoints");

					
				}
			}

			return task;
		}


		public bool AddTask(SchedulerTask task)
        {
            using (SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.CommandType = CommandType.Text;
                com.Connection = sqlCon;
                com.Parameters.AddWithValue("@UserId", task.UserId);
                com.Parameters.AddWithValue(@"Place", task.Place);
                com.Parameters.AddWithValue("@DateStart", task.DateStart);
                com.Parameters.AddWithValue("@DateEnd", task.DateEnd);
                com.Parameters.AddWithValue("@TimeLength", task.TimeLength);
                com.Parameters.AddWithValue("@TaskType", task.TaskType);
                com.Parameters.AddWithValue("@Description", task.Description);
                com.Parameters.AddWithValue("@MoneyPerHour", task.MoneyPerHour);
                com.Parameters.AddWithValue("@Bonus", task.Bonus);
                com.Parameters.AddWithValue("@IsActive", task.IsActive);
                com.Parameters.AddWithValue("@Revenue", task.Revenue);
                com.Parameters.AddWithValue("@RevenueParticipation", task.RevenueParticipation);
                com.Parameters.AddWithValue("@PremiumPoints", task.PremiumPoints);
                com.CommandText = @"INSERT INTO WorkPlannerTasks (UserId, DateStart, DateEnd, TimeLength, TaskType, Description, Place, MoneyPerHour, Bonus, IsActive, Revenue, RevenueParticipation, PremiumPoints) VALUES (@UserId, @DateStart, @DateEnd, @TimeLength, @TaskType, @Description, @Place, @MoneyPerHour, @Bonus, @IsActive, @Revenue, @RevenueParticipation, @PremiumPoints)";
                com.ExecuteNonQuery();
            }

            return true;
        }

        public List<SchedulerTask> GetSchedulerTasks(int userId, DateTime from, DateTime to)
        {
            List<SchedulerTask> tasks = new List<SchedulerTask>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = con;
                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("@DateFrom", from);
                sqlCommand.Parameters.AddWithValue("@DateTo", to);
                sqlCommand.CommandText = @"SELECT UserId, TaskId, DateStart, DateEnd, TimeLength, TaskType, Description, Place, MoneyPerHour, Bonus, IsActive, Revenue, RevenueParticipation, PremiumPoints FROM  WorkPlannerTasks WHERE UserId = @UserId AND DATESTART >= @DateFrom AND DATESTART <= @DateTo";

                var sqlData = sqlCommand.ExecuteReader();

                while (sqlData.Read())
                {
                    var task = new SchedulerTask();
                    task.UserId = sqlData.GetInt32("UserId");
                    task.TaskId = sqlData.GetInt32("TaskId");
                    task.DateStart = sqlData.GetDateTime("DateStart");
                    task.DateEnd = sqlData.GetDateTime("DateEnd");
                    task.TimeLength = sqlData.GetDouble("TimeLength");
                    task.TaskType = sqlData.GetString("TaskType");
                    task.Description = sqlData.GetString("Description");
                    task.Place = sqlData.GetString("Place");
                    task.MoneyPerHour = sqlData.GetDouble("MoneyPerHour");
                    task.Bonus = sqlData.GetDouble("Bonus");
                    task.IsActive = sqlData.GetBoolean("IsActive");
                    task.Revenue = sqlData.GetDouble("Revenue");
                    task.RevenueParticipation = sqlData.GetDouble("RevenueParticipation");
                    task.PremiumPoints = sqlData.GetDouble("PremiumPoints");

                    tasks.Add(task);
                }
            }

            return tasks;
        }

		public List<SchedulerTask> GetSchedulerGeneratorTasksWithConfig(SchedulerGeneratorConfig config)
		{
			List<SchedulerTask> tasks = new List<SchedulerTask>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.CommandType = CommandType.Text;
				sqlCommand.Connection = con;
				sqlCommand.Parameters.AddWithValue("@UserId", config.UserId);
				sqlCommand.Parameters.AddWithValue("@DateFrom", config.DateFrom);
				sqlCommand.Parameters.AddWithValue("@DateTo", config.DateTo);
				sqlCommand.CommandText = @"SELECT UserId, TaskId, DateStart, DateEnd, TimeLength, TaskType, Description, Place, MoneyPerHour, Bonus, IsActive, Revenue, RevenueParticipation, PremiumPoints FROM  WorkPlannerTasks WHERE UserId = @UserId AND DATESTART >= @DateFrom AND DATESTART <= @DateTo";

				var sqlData = sqlCommand.ExecuteReader();

				while (sqlData.Read())
				{
                    var taskType = sqlData.GetString("TaskType");

                    if (taskType == "Availabe" && !config.ShowAvailable)
                        continue;

					if (taskType == "Unavailabe" && !config.ShowUnavailable)
						continue;

					if (taskType == "Planned" && !config.ShowPlanned)
						continue;

					if (taskType == "Done" && !config.ShowDone)
						continue;

					var task = new SchedulerTask();
					task.UserId = sqlData.GetInt32("UserId");
					task.TaskId = sqlData.GetInt32("TaskId");
					task.DateStart = sqlData.GetDateTime("DateStart");
					task.DateEnd = sqlData.GetDateTime("DateEnd");
					task.TimeLength = sqlData.GetDouble("TimeLength");
                    task.TaskType = taskType;

                    if(config.ShowDescription)
                    task.Description = sqlData.GetString("Description");

					task.Place = sqlData.GetString("Place");

                    if(config.ShowIncome)
					task.MoneyPerHour = sqlData.GetDouble("MoneyPerHour");

                    if (config.ShowBonus)
                    {
						task.Bonus = sqlData.GetDouble("Bonus");
						task.Revenue = sqlData.GetDouble("Revenue");
						task.RevenueParticipation = sqlData.GetDouble("RevenueParticipation");
					}

					task.IsActive = sqlData.GetBoolean("IsActive");
					
                    if(config.ShowPremiumPoints)
					task.PremiumPoints = sqlData.GetDouble("PremiumPoints");

					tasks.Add(task);
				}
			}

			return tasks;
		}

		public bool UpdateTask(int userId, SchedulerTask task)
		{
			int rowsAffected;
			using (SqlConnection sqlCon = new SqlConnection(_connectionString))
			{
				SqlCommand com = new SqlCommand();
				sqlCon.Open();
				com.Connection = sqlCon;
				com.CommandType = CommandType.Text;
				com.Parameters.AddWithValue(@"UserId", userId);
				com.Parameters.AddWithValue(@"TaskId", task.TaskId);
                com.Parameters.AddWithValue(@"DateStart", task.DateStart);
                com.Parameters.AddWithValue(@"DateEnd", task.DateEnd);
                com.Parameters.AddWithValue(@"TimeLength", task.TimeLength);
                com.Parameters.AddWithValue(@"TaskType", task.TaskType);
                com.Parameters.AddWithValue(@"Description", task.Description);
				com.Parameters.AddWithValue(@"MoneyPerHour", task.MoneyPerHour);
				com.Parameters.AddWithValue(@"Place", task.Place);
                com.Parameters.AddWithValue(@"Bonus", task.Bonus);
                com.Parameters.AddWithValue("@Revenue", task.Revenue);
                com.Parameters.AddWithValue("@RevenueParticipation", task.RevenueParticipation);
                com.Parameters.AddWithValue("@PremiumPoints", task.PremiumPoints);
                com.CommandText = "UPDATE WORKPLANNERTASKS SET DateStart = @DateStart, DateEnd = @DateEnd, TimeLength = @TimeLength, TaskType = @TaskType, Description = @Description, Place = @Place, MoneyPerHour = @MoneyPerHour, Bonus = @Bonus, Revenue = @Revenue, RevenueParticipation = @RevenueParticipation, PremiumPoints = @PremiumPoints WHERE UserId = @UserId AND TaskId = @TaskId";
				rowsAffected = com.ExecuteNonQuery();
			}

			return rowsAffected > 0 ? true : false;
		}

		public bool DeleteTask(int userId, int taskId)
        {
            int rowsAffected;
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                SqlCommand com = new SqlCommand();
                sqlCon.Open();
                com.Connection = sqlCon;
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue(@"UserId", userId);
                com.Parameters.AddWithValue(@"TaskId", taskId);
                com.CommandText = "UPDATE WORKPLANNERTASKS SET ISACTIVE = 0 WHERE UserId = @UserId AND TaskId = @TaskId";
                rowsAffected = com.ExecuteNonQuery();
            }

            return rowsAffected > 0 ? true : false;
        }
    }
}
