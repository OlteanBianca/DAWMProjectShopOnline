using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("user")]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Private Fields
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Public Methods

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO payload)
        {
            if(payload == null)
            {
                return BadRequest("User can't be null!");
            }

            if(await _userService.FindUserByEmail(payload.Email) != null)
            {
                return BadRequest("Email is already used!");
            }

            LoginDTO? user = await _userService.Register(payload);
            if (user == null)
            {
                return BadRequest("Credentials not valid!");
            }

            string jwtToken = await _userService.Validate(user);
            return Ok(new { token = jwtToken });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO payload)
        {
            if (await _userService.FindUserByEmail(payload.Email) == null)
            {
                return BadRequest("Invalid email!");
            }

            string jwtToken = await _userService.Validate(payload);

            if (string.IsNullOrEmpty(jwtToken))
            {
                return BadRequest("Invalid password!");
            }

            return Ok(new { token = jwtToken });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            UserDTO? user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound("There is no user with that id!");
            }
            return Ok(user);
        }
        #endregion
    }
}
