using MedicalCompanyManagement.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Planner;
using WorkCalendar.Library.Utils;


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
			var userLoginId = User.GetUserId();

			DateTime dateFrom = DateTime.FromOADate(from);
			DateTime dateTo = DateTime.FromOADate(to).SetEndOfDay();

			var list = _schedulerColorService.GetColors(userLoginId, dateFrom, dateTo);

			return Ok(list);
		}
	}
}
