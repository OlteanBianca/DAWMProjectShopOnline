using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OnlineShop.DTOs;
using OnlineShop.Services;
using IAuthorizationService = OnlineShop.Services.IAuthorizationService;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("shop")]
    [Authorize]
    public class ShopController : ControllerBase
    {
        #region Private Fields
        private readonly IShopService _shopService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public ShopController(IShopService shopService, IAuthorizationService authorizationService, IUserService userService)
        {
            _shopService = shopService;
            _authorizationService = authorizationService;
            _userService = userService;
        }
        #endregion

        #region Public Methods

        [HttpPost("add")]
        public async Task<IActionResult> AddShop([FromBody] ShopDTO shopDTO)
        {
            if (shopDTO == null)
            {
                return BadRequest("Shop can't be null!");
            }

            string? accessToken = Request.Headers[HeaderNames.Authorization];
            if (accessToken == null)
            {
                return BadRequest("Access denied!");
            }

            int? id = _authorizationService.GetUserFromToken(accessToken);
            if (id == null)
            {
                return BadRequest("Invalid token!");
            }

            UserDTO? user = await _userService.GetUserById((int)id);
            if (user == null)
            {
                return NotFound("No user found!");
            }
            await _userService.UpdateUserToShopOwner((int)id);

            if (!await _shopService.AddShop(shopDTO, (int)id))
            {
                return BadRequest("Credentials not valid!");
            }
            return Ok("Shop added successfully!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _shopService.GetAllShops());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            ShopDTO? shop = await _shopService.GetShopById(id);
            if (shop == null)
            {
                return NotFound("There is no Shop with that id!");
            }
            return Ok(shop);
        }
        #endregion
    }
}
