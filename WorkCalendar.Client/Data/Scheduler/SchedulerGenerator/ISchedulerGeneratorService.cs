using WorkCalendar.Client.Data.Scheduler;

namespace WorkCalendar.Client.Data.Scheduler.SchedulerGenerator
{
	public interface ISchedulerGeneratorService
	{
		Task<List<SchedulerTask>> GetGeneratedTasks(string url);
	}
}
