using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DatabaseModels;
using WorkCalendar.Library.Planner;
using WorkCalendar.Library.Utils;
using System.Security.Claims;
using MedicalCompanyManagement.API.Extensions; // Dodane dla ClaimTypes i metod rozszerzających

namespace MedicalCompanyManagement.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlannerController : ControllerBase
    {
        private readonly IWorkPlannerService _workPlannerService;

        public WorkPlannerController(IWorkPlannerService workPlannerService)
        {
            _workPlannerService = workPlannerService;
        }

        private int CurrentUserId
            => User.GetUserId();

        [HttpGet]
        public IActionResult GetAllTasks(double from, double to)
        {
            DateTime dateFrom = DateTime.FromOADate(from);
            DateTime dateTo = DateTime.FromOADate(to).SetEndOfDay();

            var list = _workPlannerService.GetUserTasksByDate(CurrentUserId, dateFrom, dateTo);

            return Ok(list);
        }

        [HttpGet("Task")]
        public IActionResult GetAllTasks()
        {
            var list = _workPlannerService.GetAllSchedulerTasks(CurrentUserId);
            return Ok(list);
        }

        [HttpGet("TaskById")]
        public IActionResult GetTaskById(int taskId)
        {
            var task = _workPlannerService.GetUserTaskById(CurrentUserId, taskId);
            return Ok(task);
        }

        [HttpGet("TasksByDates")]
        public IActionResult GetTasksByDates([FromQuery] string dates)
        {
            var datesList = dates.Split(',')
                .Select(d => DateTime.Parse(d))
                .ToList();

            if (datesList.Count == 0)
                return NotFound();

            var task = _workPlannerService.GetUserTasksByDates(CurrentUserId, datesList);
            return Ok(task);
        }

        [HttpPost("Task")]
        public IActionResult AddTask([FromBody] SchedulerTask task)
        {
            task.UserId = CurrentUserId;

            var result = _workPlannerService.AddTask(task);

            return result ? Ok() : BadRequest();
        }

        [HttpPut]
        public IActionResult EditTask([FromBody] SchedulerTask task)
        {
            task.UserId = CurrentUserId;

            var result = _workPlannerService.UpdateTask(task);

            return result ? Ok() : BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteTask(int taskId)
        {
            var result = _workPlannerService.DeleteTask(CurrentUserId, taskId);

            return result ? Ok() : BadRequest();
        }
    }
}