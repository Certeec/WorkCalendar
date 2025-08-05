using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner.SchedulerGenerator
{
	public class SchedulerGeneratorRepository : ISchedulerGeneratorRepository
	{
		private readonly string _connectionString;

		public SchedulerGeneratorRepository(IConfiguration configuration)
		{
			_connectionString = configuration["ConnectionString"];
		}

		public SchedulerGeneratorConfig GetGeneratedConfig(string configUrl)
		{
				SchedulerGeneratorConfig generatorConfig = new SchedulerGeneratorConfig();

				using (SqlConnection con = new SqlConnection(_connectionString))
				{
					con.Open();
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.Connection = con;
					sqlCommand.Parameters.AddWithValue("@UrlKey", configUrl);
					sqlCommand.CommandText = @"SELECT UniqueId, CreatorUserId, UserId, UrlKey, IsDeleted, DeletedDate, ModificationDate, ModifiedBy, PublishFrom, PublishTo, DateFrom, DateTo, ShowAvailable, ShowUnavailable, ShowPlanned, ShowDone, ShowIncome, ShowBonus, ShowDescription, ShowPremiumPoints  FROM  GeneratedSchedulerCart WHERE UrlKey = @UrlKey";

					var sqlData = sqlCommand.ExecuteReader();

					while (sqlData.Read())
					{
						generatorConfig.UniqueId = sqlData.GetInt32("UniqueId");
						generatorConfig.CreatorUserId = sqlData.GetInt32("CreatorUserId");
						generatorConfig.UserId = sqlData.GetInt32("UserId");
						generatorConfig.UrlKey = sqlData.GetString("UrlKey");
						generatorConfig.IsDeleted = sqlData.GetBoolean("IsDeleted");
						generatorConfig.DeletedDate = sqlData.GetDateTime("DeletedDate");
						generatorConfig.ModificationDate = sqlData.GetDateTime("ModificationDate");
						generatorConfig.ModifiedBy = sqlData.GetInt32("ModifiedBy");
						generatorConfig.PublishFrom = sqlData.GetDateTime("PublishFrom");
						generatorConfig.PublishTo = sqlData.GetDateTime("PublishTo");
						generatorConfig.DateFrom = sqlData.GetDateTime("DateFrom");
						generatorConfig.DateTo = sqlData.GetDateTime("DateTo");
						generatorConfig.ShowAvailable = sqlData.GetBoolean("ShowAvailable");
						generatorConfig.ShowUnavailable = sqlData.GetBoolean("ShowUnavailable");
						generatorConfig.ShowPlanned = sqlData.GetBoolean("ShowPlanned");
						generatorConfig.ShowDone = sqlData.GetBoolean("ShowDone");
						generatorConfig.ShowIncome = sqlData.GetBoolean("ShowIncome");
						generatorConfig.ShowBonus = sqlData.GetBoolean("ShowBonus");
						generatorConfig.ShowDescription = sqlData.GetBoolean("ShowDescription");
						generatorConfig.ShowPremiumPoints = sqlData.GetBoolean("ShowPremiumPoints");
					}
				}

				return generatorConfig;
		}
	}
}
