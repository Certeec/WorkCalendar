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
        private static readonly double _defaultValue = 0;
        public UserSchedulerDefaultHourIncomeController(IUserDefaultIncomeService userDefaultIncomeService)
        {
            _userDefaultIncomeService = userDefaultIncomeService;
        }

        [HttpGet]
        public IActionResult GetUserDefaultHourIncome()
        {
            var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            var result = _userDefaultIncomeService.GetUserDefaultIncome(userLoginId);

            if (result == null)
                return Ok(_defaultValue);

            return Ok(result.MoneyPerHour);
        }

        [HttpPut]
        public IActionResult SetUserDefaultHourIncome([FromBody]double defaultHourIncome)
        {
            var userLoginId = int.Parse(HttpContext.User.Claims.First(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value.ToString());

            var result = _userDefaultIncomeService.SetUserDefaultIncome(userLoginId, defaultHourIncome);

            return result == true ? Ok(result) : BadRequest();
        }
    }
}
