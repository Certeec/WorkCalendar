namespace WorkCalendar.Api.ControllersDTO
{
    public class GameItemDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime ItemAddDate { get; set; }
        public bool IsActive { get; set; }
    }
}
