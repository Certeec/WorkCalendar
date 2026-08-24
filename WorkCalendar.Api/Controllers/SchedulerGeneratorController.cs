using MedicalCompanyManagement.API.Extensions;
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
			var userLoginId = User.GetUserId();

			var list = _schedulerGeneratorService.GetPublishedScheduler(urlKey);

			return Ok(list);
		}
	}
}
