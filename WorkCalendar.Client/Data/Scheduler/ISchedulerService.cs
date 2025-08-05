using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Scheduler
{
    public interface ISchedulerService
    {
        public Task<int> AddTask(DateTime date, double income);
        public Task<bool> EditTask(SchedulerTask task);
        public Task<bool> DeleteTask(int taskId);
        public Task<List<SchedulerTask>> GetUserTasks(DateTime from, DateTime to);
        Task<SchedulerTask> GetUserTask(int taskId);
        Task<List<SchedulerDay>> GetColorDays(DateTime from, DateTime to);
		Task<int> NewTask(DateTime date, double income);

	}
}
