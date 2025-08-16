
using DAL;
using Models.DatabaseModels;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
    public class EFWorkPlannerRepository : IWorkPlannerRepository
    {
        private readonly DatabaseContext _context;

        public EFWorkPlannerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public bool AddTask(SchedulerTask task)
        {
            _context.Set<SchedulerTask>().Add(task);
            return _context.SaveChanges() == 1;
        }

        public bool DeleteTask(int userId, int taskId)
        {
            var task = GetSchedulerTaskById(userId, taskId);
             _context.Set<SchedulerTask>().Remove(task);
            return _context.SaveChanges() == 1;
        }

        public List<SchedulerTask> GetAllSchedulerTasks(int userId)
            => _context.Set<SchedulerTask>().Where(n => n.UserId == userId).ToList(); 
       

        public List<SchedulerTask> GetSchedulerGeneratorTasksWithConfig(SchedulerGeneratorConfig config)
        {
            throw new NotImplementedException();
        }

        public SchedulerTask GetSchedulerTaskById(int userId, int taskId)
            => _context.Set<SchedulerTask>().FirstOrDefault(n => n.TaskId == taskId && n.UserId == userId);

        public List<SchedulerTask> GetSchedulerTasks(int userId, DateTime from, DateTime to)
            => _context.Set<SchedulerTask>().Where(n => n.UserId == userId && n.DateStart >= from && n.DateEnd <= to).ToList();

        public bool UpdateTask(int userId, SchedulerTask task)
        {
            var checkTask = GetSchedulerTaskById(userId, task.TaskId);
            if (!task.Equals(checkTask))
                return false;

            _context.Set<SchedulerTask>().Update(task);
            return _context.SaveChanges() == 1;
        }
    }
}
