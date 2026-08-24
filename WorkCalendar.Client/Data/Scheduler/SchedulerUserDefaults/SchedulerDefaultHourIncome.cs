using Blazored.LocalStorage;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Scheduler.SchedulerUserDefaults
{
	public class SchedulerDefaultHourIncomeService : ISchedulerDefaultHourIncomeService
	{
		private ILocalStorageService _localStorageService;
		private HttpClient _client;
		private string _serverAdress;

		public SchedulerDefaultHourIncomeService(ILocalStorageService localStorageService, HttpClient client, IConfiguration configuration)
		{
			_localStorageService = localStorageService;
			_client = client;
            _serverAdress = configuration["ServerAdress"];
        }

		public async Task<double> GetUserDefaultIncome()
		{
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

			var stringResult = await _client.GetStringAsync(_serverAdress + "UserSchedulerDefaultHourIncome");

            return double.Parse(stringResult, CultureInfo.InvariantCulture);
		}

		public async Task<bool> SetUserDefaultIncome(double income)
		{
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

			var result = await _client.PutAsJsonAsync<double>(_serverAdress + "UserSchedulerDefaultHourIncome", income);

			return true;
		}
	}
}
