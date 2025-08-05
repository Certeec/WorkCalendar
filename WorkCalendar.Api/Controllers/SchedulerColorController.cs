using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Planner;
using WorkCalendar.Library.Utils;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerColorController : ControllerBase
    {
        ISchedulerColorService _schedulerColorService;
        public SchedulerColorController(ISchedulerColorService schedulerColorService)
        {
			_schedulerColorService = schedulerColorService;
        }

		[HttpGet]
		public IActionResult GetColors(double from, double to)
		{
			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

			DateTime dateFrom = DateTime.FromOADate(from);
			DateTime dateTo = DateTime.FromOADate(to).SetEndOfDay();

			var list = _schedulerColorService.GetColors(userLoginId, dateFrom, dateTo);

			return Ok(list);
		}
	}
}
