using DAL;
using Models.DatabaseModels;

namespace WorkCalendar.Library.Planner.Places
{
    public class EFUserSchedulerPlacesService : IUserSchedulerPlacesService
    {
        private readonly DatabaseContext _dbContext;

        public EFUserSchedulerPlacesService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddUserPlace(int userId, SchedulerPlace place)
        {
            _dbContext.schedulerPlaces.Add(place);
            return _dbContext.SaveChanges() == 1;
        }

        public List<SchedulerPlace> GetUserPlaces(int userId)
        {
            return _dbContext.schedulerPlaces.Where(n => n.UserId == userId).ToList();
        }
    }
}
