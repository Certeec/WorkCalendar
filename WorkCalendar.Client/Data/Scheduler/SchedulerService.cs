using MudBlazor;
using WorkCalendar.Client.Data.Accounts.DTO;
using WorkCalendar.Client.Shared.Dialogs;

namespace WorkCalendar.Client.Data.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private IDialogService _dialogService;
        private ISchedulerTaskService _schedulerTaskService;

        public SchedulerService(IDialogService dialogService, ISchedulerTaskService schedulerService)
        {
            _dialogService = dialogService;
            _schedulerTaskService = schedulerService;
        }

        public async Task<List<SchedulerTask>> GetUserTasks(DateTime from, DateTime to)
            => await _schedulerTaskService.GetUserTasks(from, to);

		public async Task<SchedulerTask> GetUserTask(int taskId)
			=> await _schedulerTaskService.GetUserTask(taskId);


		public async Task<int> AddTask(DateTime date, double income)
        {
            var result = await SchedulerTaskDialog.OpenAddTask(_dialogService, date, income);

            if (result == null)
                return -1;

            result.IsActive = true;

            var addingResult = await _schedulerTaskService.AddTask(result);
            return result.TaskId;
        }

		public async Task<int> NewTask(DateTime date, double income)
		{
			var result = await SchedulerTaskDialog.OpenAddTask(_dialogService, date, income);

			if (result == null)
				return -1;

			result.IsActive = true;

			var addingResult = await _schedulerTaskService.AddTask(result);
			return result.TaskId;
		}

		public async Task<bool> EditTask(SchedulerTask task)
        {
            var result = await SchedulerTaskDialog.OpenEditTask(_dialogService, task);

            if (result == null)
                return false;

            var editingResult = await _schedulerTaskService.EditTask(result);
            return editingResult;
        }

        public async Task<bool> DeleteTask(int taskId)
            => await _schedulerTaskService.DeleteTask(taskId);

        public async Task<List<SchedulerDay>> GetColorDays(DateTime from, DateTime to)
            => await _schedulerTaskService.GetDaysColors(from, to);
        
    }
}
