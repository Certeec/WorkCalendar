
using Models.DatabaseModels;

namespace WorkCalendar.Library.Accounts
{
    public interface IAccountsRepository
    {
        int? GetUserID(LoginCredentialsDTO credentials);
        bool CreateUser(CreateAccountDTO accData);
        UserAccount GetUser(LoginCredentialsDTO credentials);
    }
}
