using MedicalCompanyManagement.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Accounts.UserHistory;


namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLogsController : ControllerBase
    {
        private IUserLogsRepository _userLogsRepository;

        public UserLogsController(IUserLogsRepository userLogsRepository)
        {
            _userLogsRepository = userLogsRepository;
        }

        [HttpGet("Account")]
        public IActionResult GetAllLogs()
        {
            var userLoginId = User.GetUserId();

            var list = _userLogsRepository.ReadUserLogs(userLoginId);

            return Ok(list);
        }
    }
}
