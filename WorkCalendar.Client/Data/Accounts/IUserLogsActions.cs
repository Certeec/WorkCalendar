namespace WorkCalendar.Client.Data.Accounts
{
    public interface IUserLogsActions
    {
        public Task<List<LoginHistory>> GetUserLogInHistory();
    }
}
