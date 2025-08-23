using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DatabaseModels
{
    public class UserLogInHistory
    {
        public int LogId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime? Duration { get; set; }
        public string? UserIp { get; set; }
    }
}
