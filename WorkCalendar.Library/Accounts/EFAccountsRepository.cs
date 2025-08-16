

using DAL;
using Models.DatabaseModels;

namespace WorkCalendar.Library.Accounts
{
    public class EFAccountsRepository : IAccountsRepository
    {
        private DatabaseContext _context;
        public EFAccountsRepository(DatabaseContext context)
        {
            _context = context;
        }
        public bool CreateUser(CreateAccountDTO accData)
        {
            var userAccount = new UserAccount()
            {
                Login = accData.Login,
                Password = accData.Password,
                Email = accData.Email,
                CreateDate = DateTime.Now,
                Status = true
            };

            _context.Add<UserAccount>(userAccount);
            return _context.SaveChanges() == 1;
        }

        public UserAccount GetUser(LoginCredentialsDTO credentials)
        {
            return _context.Set<UserAccount>().FirstOrDefault(n => n.Login == credentials.Login && n.Password == credentials.Password);
        }

        public int? GetUserID(LoginCredentialsDTO credentials)
        {
            var user =_context.Set<UserAccount>().FirstOrDefault(n => n.Login == credentials.Login && n.Password == credentials.Password);
            return user?.UserId;
        }
    }
}
