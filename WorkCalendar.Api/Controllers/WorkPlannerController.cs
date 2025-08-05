using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Api.ControllersDTO;
using WorkCalendar.Library.GameItems.GameItem;
using WorkCalendar.Library.GameItems.ItemReadings;


namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GameItemsController : ControllerBase
    {
        private IGameItemService _gameItemsService;
        private readonly ItemReadingsRepository _readingsRepository;

        public GameItemsController(IGameItemService gameItemsService)
        {
            _gameItemsService = gameItemsService;
        }

        [HttpGet("Task")]
        public IActionResult GetAllItems()
        {
			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            var items = _gameItemsService.GetAllItems();

            var readings = _readingsRepository.GetReadings(); // 1 xx 2 yy 3 yy

            var readingsGrouped = readings.GroupBy(x => x.ItemId).ToDictionary(x => x.Key, x => x.ToList());

            items.Select(item => new ItemPriceChangeDTO
            {
                GameItem = new GameItemDTO
                {
                    //...
                },
                PriceReads = readingsGrouped[item.ItemId].Select(x => new PriceReadingDTO { }).ToList()
            });

            return Ok(_gameItemsService.GetAllItems());
        }
    }
}
