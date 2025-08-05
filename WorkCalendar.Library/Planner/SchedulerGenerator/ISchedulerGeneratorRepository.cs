using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner.SchedulerGenerator
{
	public interface ISchedulerGeneratorRepository
	{
		public SchedulerGeneratorConfig GetGeneratedConfig(string configUrl);
	}
}
