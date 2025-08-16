using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DatabaseModels
{
    [Table("WorkPlannerTasks")]
    public class SchedulerTask
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        [NotMapped]
        public bool ShowDetails { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public double TimeLength { get; set; }
        public string Place { get; set; }
        public string TaskType { get; set; }
        public string? Description { get; set; }
        public double MoneyPerHour { get; set; }
        public double Bonus { get; set; }
        public bool IsActive { get; set; }
        public double Revenue { get; set; }
        public double RevenueParticipation { get; set; }
        public double PremiumPoints { get;set; }
    }
}
