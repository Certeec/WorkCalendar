using Models.DatabaseModels;

namespace WorkCalendar.Library.Accounts.UserHistory
{
    public interface IUserLogsRepository
    {
        public void RegisterLoginIn(int userId);
        public List<UserLogInHistory> ReadUserLogs(int UserId);
    }
}
