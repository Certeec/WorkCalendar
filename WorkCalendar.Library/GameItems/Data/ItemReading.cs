namespace WorkCalendar.Library.GameItems.Data
{
    public class ItemReading
    {
        public int ReadingId { get; set; }
        public int ItemId { get; set; }
        public double Value { get; set; }
        public DateTime ReadingAddDate { get; set; }
        public int ReadingAddedBy { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
