using WorkCalendar.Client.Data.Accounts.DTO;

namespace WorkCalendar.Client.Data.Scheduler
{
    public interface ISchedulerTaskService
    {
		Task<List<SchedulerTask>> GetUserTasks(DateTime from, DateTime To);
		Task<SchedulerTask> GetUserTask(int id);
		Task<bool> AddTask(SchedulerTask task);
        Task<bool> EditTask(SchedulerTask task);
        Task<bool> DeleteTask(int taskId);
		Task<List<SchedulerDay>> GetDaysColors(DateTime from, DateTime to);
	}
}
