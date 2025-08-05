using WorkCalendar.Library.Models;

namespace WorkCalendar.Library.Planner.SchedulerGenerator
{
	public class SchedulerGeneratorService : ISchedulerGeneratorService
	{
		private ISchedulerGeneratorRepository _schedulerGeneratorRepository;
		private IWorkPlannerRepository _workPlannerRepository;

		public SchedulerGeneratorService(ISchedulerGeneratorRepository schedulerGeneratorRepository, IWorkPlannerRepository workPlannerRepository)
		{
			_schedulerGeneratorRepository = schedulerGeneratorRepository;
			_workPlannerRepository = workPlannerRepository;
		}

		public List<SchedulerTask> GetPublishedScheduler(string url)
		{
			var config = _schedulerGeneratorRepository.GetGeneratedConfig(url);
			if (config == null)
				return new List<SchedulerTask>();

			return _workPlannerRepository.GetSchedulerGeneratorTasksWithConfig(config);
		}
	}
}
