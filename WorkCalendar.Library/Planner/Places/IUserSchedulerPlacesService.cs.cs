
using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.Places
{
    public interface IUserSchedulerPlacesService
    {
        List<SchedulerPlace> GetUserPlaces(int userId);
        bool AddUserPlace(SchedulerPlace place);
    }
}
