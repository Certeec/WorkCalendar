using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkCalendar.Library.Accounts.Safety;
using WorkCalendar.Library.Accounts.UserHistory;


namespace WorkCalendar.Library.Accounts
{
    public class UserService : IUserService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IConfiguration _configuration;
        private readonly IHasher _hasher;
        private readonly IUserLogsRepository _userLogsRepository;

        public UserService(IAccountsRepository accountRepository, IHasher hasher, IConfiguration configuration, IUserLogsRepository userLogsRepository)
        {
            _accountsRepository = accountRepository;
            _hasher = hasher;
            _configuration = configuration;
            _userLogsRepository = userLogsRepository;
        }

        public UserToken Login(LoginCredentialsDTO credentials)
        {
            credentials.Password = _hasher.HashText(credentials.Password);
            var user = _accountsRepository.GetUser(credentials);
            if (user.Login == null)
            {
                return new UserToken();
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            DateTime tokenExpires = DateTime.Now.AddMinutes(50);
            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim(ClaimTypes.GivenName, "Bearer"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                }),
                IssuedAt = DateTime.Now,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = tokenExpires,
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            Console.WriteLine("user token" + token);

            user.Token = tokenHandler.WriteToken(token);

            _userLogsRepository.RegisterLoginIn(user.UserId);

            UserToken userToken = new UserToken();
            userToken.Token = user.Token;
            userToken.Expires = tokenExpires;

            return userToken;
        }

        public bool CreateUser(CreateAccountDTO accData)
        {
            accData.Password = _hasher.HashText(accData.Password);

            return _accountsRepository.CreateUser(accData);
        }
    }
}
