using MedicalCompanyManagement.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Planner.ConfigurableDefaultvalues;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkCalendar.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSchedulerDefaultHourIncomeController : ControllerBase
    {
        IUserDefaultIncomeService _userDefaultIncomeService;
        private const double _defaultValue = 0;
        public UserSchedulerDefaultHourIncomeController(IUserDefaultIncomeService userDefaultIncomeService)
        {
            _userDefaultIncomeService = userDefaultIncomeService;
        }

        [HttpGet]
        public IActionResult GetUserDefaultHourIncome()
        {
            var userLoginId = User.GetUserId();

            var result = _userDefaultIncomeService.GetUserDefaultIncome(userLoginId);

            if (result == null)
                return Ok(_defaultValue);

            return Ok(result.MoneyPerHour);
        }

        [HttpPut]
        public IActionResult SetUserDefaultHourIncome([FromBody]double defaultHourIncome)
        {
            var userLoginId = User.GetUserId();

            var result = _userDefaultIncomeService.SetUserDefaultIncome(userLoginId, defaultHourIncome);

            return result ? Ok(result) : BadRequest();
        }
    }
}
