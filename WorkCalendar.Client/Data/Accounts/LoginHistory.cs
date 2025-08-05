namespace WorkCalendar.Client.Data.Accounts
{
    public class LoginHistory
    {
        public int UserId { get; set; }
        public DateTime date { get; set; }
        public DateTime duration { get; set; }
        public string ip { get; set; }
    }
}
