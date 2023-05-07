using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("login")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        #region Private Fields
        //private readonly IUserService _userService;
        #endregion

        #region Constructors
        public LoginController()
        {
            //_userService = userService;
        }
        #endregion

        #region Public Methods

        //[HttpPost("/register")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Register(RegisterDTO payload)
        //{
        //    LoginDTO? user = await _userService.Register(payload);

        //    if (user == null)
        //    {
        //        return BadRequest("Credentials not valid!");
        //    }

        //    string jwtToken = await _userService.Validate(user);
        //    return Ok(new { token = jwtToken });
        //}

        //[HttpPost("/login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login(LoginDTO payload)
        //{
        //    if (await _userService.FindUserByEmail(payload.Email) == null)
        //    {
        //        return BadRequest("Invalid email!");
        //    }

        //    string jwtToken = await _userService.Validate(payload);

        //    if (string.IsNullOrEmpty(jwtToken))
        //    {
        //        return BadRequest("Invalid password!");
        //    }

        //    return Ok(new { token = jwtToken });
        //}
        #endregion
    }
}
