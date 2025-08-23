using DAL;
using Models.DatabaseModels;

namespace WorkCalendar.Library.Accounts.UserHistory
{
    public class EFUserLogsRepository : IUserLogsRepository
    {
        private DatabaseContext _context;
        public EFUserLogsRepository(DatabaseContext context) 
        {
            _context = context;   
        }

        public List<UserLogInHistory> ReadUserLogs(int UserId)
        {
            return _context.Set<UserLogInHistory>().Where(n => n.UserId == UserId).ToList();
        }

        public void RegisterLoginIn(int userId)
        {
            var userLogInHistory = new UserLogInHistory()
            {
                UserId = userId,
                LoginDate = DateTime.Now,
                Duration = DateTime.MinValue,
                UserIp = string.Empty
            };
            _context.Set<UserLogInHistory>().Add(userLogInHistory);
            _context.SaveChanges();
        }
    }
}
