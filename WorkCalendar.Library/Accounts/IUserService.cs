using WorkCalendar.Library.Accounts.Safety;

namespace WorkCalendar.Library.Accounts
{
    public interface IUserService
    {
        public UserToken Login(LoginCredentialsDTO credentials);
        bool CreateUser(CreateAccountDTO accData);
    }
}
