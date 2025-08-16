using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner
{
    public interface IWorkPlannerService
    {
        public List<SchedulerTask> GetAllSchedulerTasks(int userId);
        public bool AddTask(SchedulerTask task);
        public bool UpdateTask(int userId, SchedulerTask task);
        public List<SchedulerTask> GetUserTasksByDate(int userId, DateTime from, DateTime to);
        public SchedulerTask GetUserTaskById(int userId, int taskId);
        bool DeleteTask(int userId, int taskId);
	}
}
