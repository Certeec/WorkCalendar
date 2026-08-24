using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.SchedulerGenerator
{
	public class SchedulerGeneratorService : ISchedulerGeneratorService
	{
		private ISchedulerGeneratorRepository _schedulerGeneratorRepository;

		public SchedulerGeneratorService(ISchedulerGeneratorRepository schedulerGeneratorRepository)
		{
			_schedulerGeneratorRepository = schedulerGeneratorRepository;
		}

        public List<SchedulerTask> GetPublishedScheduler(string url)
        {
            throw new NotImplementedException();
        }

        //public List<SchedulerTask> GetPublishedScheduler(string url)
        //{
        //	var config = _schedulerGeneratorRepository.GetGeneratedConfig(url);
        //	if (config == null)
        //		return new List<SchedulerTask>();

        //	//return _workPlannerRepository.GetSchedulerGeneratorTasksWithConfig(config);
        //}
    }
}
