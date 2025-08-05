using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Accounts
{
    public class UserLogsActions : IUserLogsActions
    {
        private IHttpClientFactory _clientFactory;
        private HttpClient _client;
        private ILocalStorageService _localStorageService;
        private string _serverAdress;

        public UserLogsActions(IHttpClientFactory clientFactory, ILocalStorageService localStorageService, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient();
            _localStorageService = localStorageService;
            _serverAdress = configuration["ServerAdress"];
        }

        public async Task<List<LoginHistory>> GetUserLogInHistory()
        {
            var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

            return await _client.GetFromJsonAsync<List<LoginHistory>>(_serverAdress + "UserLogs/Account");
        }
    }
}
