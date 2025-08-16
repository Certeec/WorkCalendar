

using Models.DatabaseModels;

namespace WorkCalendar.Client.Data.Accounts
{
    public interface IUserLogsActions
    {
        public Task<List<UserLogInHistory>> GetUserLogInHistory();
    }
}
