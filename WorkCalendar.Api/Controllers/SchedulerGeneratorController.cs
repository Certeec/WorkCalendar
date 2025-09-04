using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Planner.SchedulerGenerator;


namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerGeneratorController : ControllerBase
    {
        ISchedulerGeneratorService _schedulerGeneratorService;

		public SchedulerGeneratorController(ISchedulerGeneratorService schedulerGeneratorService)
		{
			_schedulerGeneratorService = schedulerGeneratorService;
		}

		[HttpGet]
		public IActionResult GetAllTasks(string urlKey)
		{
			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

			var list = _schedulerGeneratorService.GetPublishedScheduler(urlKey);

			return Ok(list);
		}
	}
}
