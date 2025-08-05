namespace WorkCalendar.Library.Accounts.UserHistory
{ 
	public class UserLogInHistory
    {
        public int UserId { get; set; }
        public DateTime date { get; set; }
        public DateTime duration { get; set; }
        public string ip { get; set; }
    }
}
