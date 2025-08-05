using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Accounts
{
    public interface IAccountsRepository
    {
        int? GetUserID(LoginCredentialsDTO credentials);
        bool CreateUser(CreateAccountDTO accData);
        UserAccount GetUser(LoginCredentialsDTO credentials);
    }
}
