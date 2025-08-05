using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Scheduler
{
    public class SchedulerTaskService : ISchedulerTaskService
    {
        ILocalStorageService _localStorageService;
        private HttpClient _client;
		private string _serverAdress;

        public SchedulerTaskService(ILocalStorageService localStorageService, HttpClient client, IConfiguration configuration)
        {
            _localStorageService = localStorageService;
            _client = client;
            _serverAdress = configuration["ServerAdress"];
        }

        public async Task<List<SchedulerTask>> GetUserTasks(DateTime from, DateTime to)
        {
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

            double fromToDouble = from.Date.ToOADate();
            double toToDouble = to.Date.ToOADate();

            var apiString = _serverAdress + "WorkPlanner?from=" + fromToDouble + "&to=" + toToDouble;


			var result = await _client.GetFromJsonAsync<List<SchedulerTask>>(apiString);
            return result;
        }

		public async Task<SchedulerTask> GetUserTask(int taskId)
		{
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

			var apiString = _serverAdress + "WorkPlanner/TaskById?taskId=" + taskId;


			var result = await _client.GetFromJsonAsync<SchedulerTask>(apiString);
			return result;
		}

		public async Task<bool> AddTask(SchedulerTask task)
        {
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);
            HttpResponseMessage response = await _client.PostAsJsonAsync(_serverAdress + "WorkPlanner/Task", task);

            return response.StatusCode.ToString() == "OK" ? true : false;
        }

		public async Task<bool> EditTask(SchedulerTask task)
		{
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);
			HttpResponseMessage response = await _client.PutAsJsonAsync(_serverAdress + "WorkPlanner", task);

			return response.StatusCode.ToString() == "OK" ? true : false;
		}

		public async Task<bool> DeleteTask(int taskId)
        {
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");
            string apiString = _serverAdress + "WorkPlanner?taskId=" + taskId;

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);
			HttpResponseMessage response = await _client.DeleteAsync(apiString);

			return response.StatusCode.ToString() == "OK" ? true : false;
		}

		public async Task<List<SchedulerDay>> GetDaysColors(DateTime from, DateTime to)
		{
			var userToken = await _localStorageService.GetItemAsync<UserToken>("UserAuthToken");

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken.Token);

			double fromToDouble = from.Date.ToOADate();
			double toToDouble = to.Date.ToOADate();

			var apiString = _serverAdress + "SchedulerColor?from=" + fromToDouble + "&to=" + toToDouble;

			var result = await _client.GetFromJsonAsync<List<SchedulerDay>>(apiString);
			return result;
		}
	}
}
