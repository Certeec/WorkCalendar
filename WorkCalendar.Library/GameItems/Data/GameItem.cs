namespace WorkCalendar.Library.GameItems.Data
{
    public class GameItem
    {
       
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public DateTime ItemAddDate { get; set; }
        public int ItemAddedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
