using DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseModels;
using WorkCalendar.Library.Planner.Places;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerPlacesController : ControllerBase
    {
        IUserSchedulerPlacesService _userSchedulerPlacesService;
        public SchedulerPlacesController(IUserSchedulerPlacesService userschedulerPlacesService)
        {
            _userSchedulerPlacesService = userschedulerPlacesService;
        }

        [HttpGet]
        public IActionResult GetUserPlaces()
        {
            int userLoginId = -1;

			try
            {
				userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

			}
			catch (Exception ex)
            {
                Console.WriteLine("Found exception" + ex.Message);
            }

            if(userLoginId != -1){
				var result = _userSchedulerPlacesService.GetUserPlaces(userLoginId);
                List<SchedulerPlaceDTO> schedulerPlaceDTOs = result.Select(place => new SchedulerPlaceDTO
                {
                    PlaceId = place.PlaceId,
                    Name = place.PlaceName,
                    IsActive = place.IsActive
                }).ToList();

				return Ok(schedulerPlaceDTOs);
			};

            return BadRequest();
        }

        [HttpPost]
        public IActionResult AddUserPlace([FromBody]SchedulerPlaceDTO placeDTO)
        {
			int userLoginId = -1;

			try
			{
				userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

			}
			catch (Exception ex)
			{
				Console.WriteLine("Found exception" + ex.Message);
			}

            SchedulerPlace place = new SchedulerPlace()
            {
                PlaceId = placeDTO.PlaceId,
                PlaceName = placeDTO.Name,
                IsActive = placeDTO.IsActive,
            };
            
			var result = _userSchedulerPlacesService.AddUserPlace(userLoginId, place);

            return result == true ? Ok(result) : BadRequest();
        }
    }
}
