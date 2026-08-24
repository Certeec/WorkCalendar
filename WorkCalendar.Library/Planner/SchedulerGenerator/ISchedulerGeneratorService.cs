using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.SchedulerGenerator
{
	public interface ISchedulerGeneratorService
	{
		List<SchedulerTask> GetPublishedScheduler(string url);
	}
}
