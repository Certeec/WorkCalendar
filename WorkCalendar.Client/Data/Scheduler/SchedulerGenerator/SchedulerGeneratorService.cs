using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WorkCalendar.Client.Data.Accounts.DTO;
using WorkCalendar.Client.Data.Scheduler;

namespace WorkCalendar.Client.Data.Scheduler.SchedulerGenerator
{
	public class SchedulerGeneratorService : ISchedulerGeneratorService
	{
		ILocalStorageService _localStorageService;
		private HttpClient _client;
		private string _serverAdress;

		public SchedulerGeneratorService(ILocalStorageService localStorageService, HttpClient client, IConfiguration configuration)
		{
			_localStorageService = localStorageService;
			_client = client;
			_serverAdress = configuration["ServerAdress"];
		}

		public async Task<List<SchedulerTask>> GetGeneratedTasks(string url)
		{
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

			var apiString = _serverAdress + "SchedulerGenerator?urlKey=" + url;


			var result = await _client.GetFromJsonAsync<List<SchedulerTask>>(apiString);
			return result;
		}
	}
}
