using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DatabaseModels
{
    public class UserAccount
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string Email { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Token { get; set; }
    }
}
