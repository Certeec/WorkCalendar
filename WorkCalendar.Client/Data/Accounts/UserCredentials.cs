namespace WorkCalendar.Client.Data.Accounts
{
    public class UserCredentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool VerifyData()
        {
            if (Login == null || Login == string.Empty)
                return false;

            if (Password == null || Password == string.Empty)
                return false;

            if (Email == null || Email == string.Empty)
                return false;

            if(false == Email.Contains("@"))
            {
                return false;
            }

            return true;
        }
    }
}
