namespace WorkCalendar.Client.Data.Scheduler.SchedulerPlaces
{
    public interface ISchedulerPlacesService
    {
        Task<List<SchedulerPlace>> GetUserPlaces();
        void AddUserPlace(SchedulerPlace place);
    }
}
