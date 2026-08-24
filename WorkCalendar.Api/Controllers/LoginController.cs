using Microsoft.AspNetCore.Mvc;
using WorkCalendar.Library.Accounts;


namespace WorkCalendar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Account")]
        public IActionResult CreateAccount([FromBody] CreateAccountDTO accData)
        {
            var result = _userService.CreateUser(accData);
            return result ? Ok() : BadRequest();
        }

        [HttpPost]
        public IActionResult LoginIn([FromBody] LoginCredentialsDTO loginData)
        {
            var result = _userService.Login(loginData);
              
            return result.Token == null ? NotFound() : Ok(result);
        }

        [HttpDelete]
        public IActionResult LogoutUser(string key)
        {
            return NoContent();
        }
    }
}
