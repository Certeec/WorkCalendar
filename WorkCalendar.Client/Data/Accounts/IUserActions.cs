using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Accounts
{
    public interface IUserActions
    {
        public Task<UserToken> LoginIn(string username, string password);
        public Task<bool> CreateAccount(UserCredentials userCredentials);
    }
}
