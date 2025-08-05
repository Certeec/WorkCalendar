namespace WorkCalendar.Api.ControllersDTO
{
    public class ItemPriceChangeDTO
    {
        public GameItemDTO GameItem { get; set; }

        public  List<PriceReadingDTO> PriceReads { get; set; }
    }
}
