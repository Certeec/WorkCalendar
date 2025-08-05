using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
    public interface IWorkPlannerRepository
    {
        List<SchedulerTask> GetAllSchedulerTasks(int userId);
        bool AddTask(SchedulerTask task);
		bool UpdateTask(int userId, SchedulerTask task);
		List<SchedulerTask> GetSchedulerTasks(int userId, DateTime from, DateTime to);
        SchedulerTask GetSchedulerTaskById(int userId, int taskId);
        bool DeleteTask(int userId, int taskId);
        List<SchedulerTask> GetSchedulerGeneratorTasksWithConfig(SchedulerGeneratorConfig config);
	}
}
