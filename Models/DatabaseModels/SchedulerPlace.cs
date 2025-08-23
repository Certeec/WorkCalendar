
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DatabaseModels
{
    public class SchedulerPlace
    {
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public string PlaceName { get; set; }
        public bool IsActive { get; set; }
    }
}
