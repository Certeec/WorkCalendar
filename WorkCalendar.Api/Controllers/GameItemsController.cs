
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Models;
using WorkCalendar.Library.Planner;
using WorkCalendar.Library.Utils;


namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlannerController : ControllerBase
    {
        private IWorkPlannerService _workPlannerService;

        public WorkPlannerController(IWorkPlannerService workPlannerService)
        {
            _workPlannerService = workPlannerService;
        }

        [HttpGet]
        public IActionResult GetAllTasks(double from, double to)
        {
            var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            DateTime dateFrom = DateTime.FromOADate(from);
            DateTime dateTo = DateTime.FromOADate(to).SetEndOfDay();

			var list = _workPlannerService.GetUserTasksByDate(userLoginId, dateFrom, dateTo);

            return Ok(list);
        }

        [HttpGet("Task")]
        public IActionResult GetAllTasks()
        {
			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            var list = _workPlannerService.GetAllSchedulerTasks(userLoginId);

            return Ok(list);
        }

		[HttpGet("TaskById")]
		public IActionResult GetTaskById(int taskId)
		{
			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());


            var task = _workPlannerService.GetUserTaskById(userLoginId, taskId);

			return Ok(task);
		}


		[HttpPost("Task")]
        public IActionResult AddTask([FromBody]SchedulerTask task)
        {

			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            task.UserId = userLoginId;

            var result = _workPlannerService.AddTask(task);

			return result == true ? Ok() : BadRequest();
		}

        [HttpPut]
        public IActionResult EditTask([FromBody]SchedulerTask task)
        {
			var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            var result = _workPlannerService.UpdateTask(userLoginId, task);

			return result == true ? Ok() : BadRequest();
		}

        [HttpDelete]
        public IActionResult DeleteTask(int taskId)
        {
            var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            var result = _workPlannerService.DeleteTask(userLoginId, taskId);

            return result == true ? Ok() : BadRequest();
        }
    }
}
