using Microsoft.IdentityModel.Tokens;

namespace WorkCalendar.Client.Data.Accounts
{
    public class UserCredentials
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public bool VerifyData()
        {
            if (Login.IsNullOrEmpty())
                return false;

            if (Password.IsNullOrEmpty())
                return false;

            if (Email.IsNullOrEmpty())
                return false;

            if(!Email.Contains("@"))
            {
                return false;
            }

            return true;
        }
    }
}
