namespace WorkCalendar.Library.Models
{
	public class SchedulerGeneratorConfig
	{
		public int UniqueId { get; set; }
		public int CreatorUserId {  get; set; }
		public int UserId {  get; set; }
		public string UrlKey { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime DeletedDate {  get; set; }
		public DateTime ModificationDate { get; set; }
		public int ModifiedBy { get; set; }
		public DateTime PublishFrom { get; set; }
		public DateTime PublishTo { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public bool ShowAvailable { get; set; }
		public bool ShowUnavailable { get; set; }
		public bool ShowPlanned { get; set; }
		public bool ShowDone { get; set; }
		public bool ShowIncome { get; set; }
		public bool ShowBonus { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowPremiumPoints { get; set; }
	}
}
