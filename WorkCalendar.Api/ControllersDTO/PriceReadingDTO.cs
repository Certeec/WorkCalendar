namespace WorkCalendar.Api.ControllersDTO
{
    public class PriceReadingDTO
    {
        public double Value { get; set; }
        public DateTime ReadingAddDate { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
