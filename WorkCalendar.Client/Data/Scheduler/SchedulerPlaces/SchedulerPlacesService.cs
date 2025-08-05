using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Scheduler.SchedulerPlaces
{
    public class SchedulerPlacesService : ISchedulerPlacesService
    {
        HttpClient _client;
        ILocalStorageService _localStorageService;
        private string _serverAdress;

        public SchedulerPlacesService(HttpClient httpClient, ILocalStorageService localStorageService, IConfiguration configuration)
        {
            _client = httpClient;
            _localStorageService = localStorageService;
            _serverAdress = configuration["ServerAdress"];
        }

        public async Task<List<SchedulerPlace>> GetUserPlaces()
        {
            var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

            var result = await _client.GetFromJsonAsync<List<SchedulerPlace>>(_serverAdress +"SchedulerPlaces");

            return result;
        }

        public async void AddUserPlace(SchedulerPlace place)
        {
            var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

            var result = await _client.PostAsJsonAsync<SchedulerPlace>(_serverAdress + "SchedulerPlaces", place);
        }
    }
}
