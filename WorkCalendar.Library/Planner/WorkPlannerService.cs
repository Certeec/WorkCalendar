using DAL;
using Microsoft.EntityFrameworkCore;
using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner
{
    public class WorkPlannerService : IWorkPlannerService
    {
        private readonly DatabaseContext _context;

        public WorkPlannerService(DatabaseContext context)
        {
            _context = context;
        }

        public bool AddTask(SchedulerTask task)
        {
            _context.schedulerTasks.Add(task);
            return _context.SaveChanges() == 1;
        }
            
        public bool UpdateTask(SchedulerTask task)
        {
            var checkTask = GetUserTaskById(task.UserId,task.TaskId);
            if (!task.UserId.Equals(checkTask.UserId))
                return false;

            _context.Entry(checkTask).State = EntityState.Detached;

            _context.schedulerTasks.Update(task);
            return _context.SaveChanges() == 1;
        }

        public List<SchedulerTask> GetAllSchedulerTasks(int userId)
            => _context.schedulerTasks.Where(n => n.UserId == userId).ToList();

        public SchedulerTask GetUserTaskById(int userId, int taskId)
            => _context.schedulerTasks.FirstOrDefault(n => n.TaskId == taskId && n.UserId == userId);

        public List<SchedulerTask> GetUserTasksByDate(int userId, DateTime from, DateTime to)
           => _context.schedulerTasks.Where(n => n.UserId == userId && n.DateStart >= from && n.DateEnd <= to).ToList();

        public bool DeleteTask(int userId, int taskId)
        {
            var task = GetUserTaskById(userId, taskId);
            _context.schedulerTasks.Remove(task);
            return _context.SaveChanges() == 1;
        }
    }
}
