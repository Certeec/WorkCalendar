using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
    public class WorkPlannerService : IWorkPlannerService
    {
        IWorkPlannerRepository _workPlannerRepository;

        public WorkPlannerService(IWorkPlannerRepository workPlannerRepository)
        {
            _workPlannerRepository = workPlannerRepository;
        }

        public bool AddTask(SchedulerTask task)
            => _workPlannerRepository.AddTask(task);

        public bool UpdateTask(int userId, SchedulerTask task)
            => _workPlannerRepository.UpdateTask(userId, task);

        public List<SchedulerTask> GetAllSchedulerTasks(int userId)
            => _workPlannerRepository.GetAllSchedulerTasks(userId);

		public SchedulerTask GetUserTaskById(int userId, int taskId)
			=> _workPlannerRepository.GetSchedulerTaskById(userId, taskId);

		public List<SchedulerTask> GetUserTasksByDate(int userId, DateTime from, DateTime to)
            => _workPlannerRepository.GetSchedulerTasks(userId, from, to).Where(n => n.IsActive).ToList();
        
        public bool DeleteTask(int userId, int taskId)
            => _workPlannerRepository.DeleteTask(userId, taskId);
    }
}
