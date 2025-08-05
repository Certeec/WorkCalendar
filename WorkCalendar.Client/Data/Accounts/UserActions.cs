using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using WorkCalendar.Client.Data.Accounts.DTO;
using WorkCalendar.Client.Data.Auth;


namespace WorkCalendar.Client.Data.Accounts
{
    public class UserActions : IUserActions
    {
        private IHttpClientFactory _clientFactory;
        private HttpClient _client;
        private ILocalStorageService _localStorageService;
        private AuthStateProvider _authStateProvider;
        private string _serverAdress;

        public UserActions(IHttpClientFactory clientFactory, ILocalStorageService localStorageService, AuthenticationStateProvider authStateProvider, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient();
            _localStorageService = localStorageService;
            _authStateProvider = (AuthStateProvider)authStateProvider;
            _client.Timeout = new TimeSpan(0,0,5);
            _serverAdress = configuration["ServerAdress"];
        }


        public async Task<UserToken> LoginIn(string username, string password)
        {

            
            var result = await LoginInUser(username, password);

            await _localStorageService.SetItemAsync("UserAuthToken", result);

            _authStateProvider.NotifyUserAuthentication(result);
            //TO add Mail

            return result;
        }

        private async Task<UserToken> LoginInUser(string username, string password)
        {
            var user = new UserToSerialize() { Login = username, Password = password };
            try
            {
                HttpResponseMessage response = await _client.PostAsJsonAsync(_serverAdress + "Login", user);
                Console.WriteLine("Response message new way is  CODE STATUS " + response.StatusCode);
                if (response.StatusCode.ToString() == "OK")
                {
                    return await response.Content.ReadFromJsonAsync<UserToken>();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new UserToken();
            }

            return new UserToken();

        }
        public async Task<bool> CreateAccount(UserCredentials userCredentials)
        {
            HttpResponseMessage response = await _client.PostAsJsonAsync(_serverAdress + "Login/Account", userCredentials);

            return response.StatusCode.ToString() == "OK" ? true : false;
        }
    }
}
