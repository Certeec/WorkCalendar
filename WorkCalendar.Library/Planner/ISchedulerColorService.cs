
using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner
{
	public interface ISchedulerColorService
	{
		List<SchedulerDay> GetColors(int userId, DateTime from, DateTime to);
	}
}
