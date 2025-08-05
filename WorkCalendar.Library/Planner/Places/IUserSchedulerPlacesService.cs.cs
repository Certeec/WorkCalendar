using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkCalendar.Library.Planner.Places
{
    public interface IUserSchedulerPlacesService
    {
        List<SchedulerPlace> GetUserPlaces(int userId);
        bool AddUserPlace(int userId, SchedulerPlace place);
    }
}
