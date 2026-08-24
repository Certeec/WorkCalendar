

using DTOModels;

namespace WorkCalendar.Client.Data.Scheduler.SchedulerPlaces
{
    public interface ISchedulerPlacesService
    {
        Task<List<SchedulerPlaceDTO>> GetUserPlaces();
        void AddUserPlace(SchedulerPlaceDTO place);
    }
}
