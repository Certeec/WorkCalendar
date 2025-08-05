using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace WorkCalendar.Library.GameItems.GameItem
{
    public class GameItemRepository : IGameItemRepository
    {
        private readonly string _connectionString;

        public GameItemRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        public List<Data.GameItem> GetGameItems()
        {
            List<Data.GameItem> gameItems = new List<Data.GameItem>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                List<int> items = new List<int>();
                string itmesToRead = string.Join(",", items); // WAŻŃE JAK LISTA PUSTA TO PUSTY STRING -> "IN ()"

                con.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = con;
                sqlCommand.CommandText = $"SELECT ItemId, ItemName, Description, ItemAddDate, ItemAddedBy, IsActive FROM  GameItems WHERE ItemId IN ({itmesToRead})";

                var sqlData = sqlCommand.ExecuteReader();



                while (sqlData.Read())
                {
                    
                    var gameItem = new Data.GameItem();
                    gameItem.ItemId = sqlData.GetInt32("ItemId");
                    gameItem.ItemName = sqlData.GetString("ItemName");
                    gameItem.Description = sqlData.GetString("Description");
                    gameItem.ItemAddDate = sqlData.GetDateTime("ItemAddDate");
                    gameItem.ItemAddedBy = sqlData.GetInt32("ItemAddedBy");
                    gameItem.IsActive = sqlData.GetBoolean("IsActive");

                    gameItems.Add(gameItem);
                }
            }

            return gameItems;
        }
    }
}

