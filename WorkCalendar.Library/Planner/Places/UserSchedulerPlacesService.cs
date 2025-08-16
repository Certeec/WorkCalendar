using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.DatabaseModels;
using System.Data;
using System.Text;

namespace WorkCalendar.Library.Planner.Places
{
    public class UserSchedulerPlacesService : IUserSchedulerPlacesService
    {
        private readonly string _connectionString;
        public UserSchedulerPlacesService(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public List<SchedulerPlace> GetUserPlaces(int userId)
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue(@"UserId", userId);
                cmd.CommandText = "SELECT PlaceId, UserId, PlaceName, IsActive FROM USERSCHEDULERPLACES WHERE Userid = @UserId";
                var reader = cmd.ExecuteReader();

                List<SchedulerPlace> places = new List<SchedulerPlace>();

                while (reader.Read())
                {
                    var place = new SchedulerPlace();
                    place.PlaceId = reader.GetInt32("PlaceId");
                    place.PlaceName = reader.GetString("PlaceName");
                    place.IsActive = reader.GetBoolean("IsActive");

                    places.Add(place);
                }

                return places;
            }
        }

        public bool AddUserPlace(int userId, SchedulerPlace place)
        {
            int result;
            using(SqlConnection sqlCon = new SqlConnection(_connectionString))
            {
                sqlCon.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = sqlCon;
                com.CommandType = CommandType.Text;
                com.Parameters.AddWithValue(@"UserId", userId);
                com.Parameters.AddWithValue(@"PlaceName", place.PlaceName);
                com.Parameters.AddWithValue(@"IsActive", 1);
                com.CommandText = "INSERT INTO USERSCHEDULERPLACES (UserId, PlaceName, IsActive) VALUES (@UserId, @PlaceName, @IsActive)";
                result = com.ExecuteNonQuery();
            }
            return result == 0 ? false : true; 
        }
    }
}
