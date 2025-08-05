using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Planner.SchedulerGenerator;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
