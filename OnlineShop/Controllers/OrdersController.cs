using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OnlineShop.DTOs;
using OnlineShop.Services;
using IAuthorizationService = OnlineShop.Services.IAuthorizationService;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("order")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        #region Private Fields
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public OrdersController(IOrderService orderService, IAuthorizationService authorizationService, IUserService userService)
        {
            _orderService = orderService;
            _authorizationService = authorizationService;
            _userService = userService;
        }
        #endregion

        #region Public Methods
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _orderService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            OrderDTO? order = await _orderService.GetById(id);
            if (order == null)
            {
                return NotFound("There is no order with that id!");
            }
            return Ok(order);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            List<OrderDTO?> orders = await _orderService.GetByUserId(id);
            if (orders == null || orders.Count == 0)
            {
                return NotFound("The user has no orders!");
            }
            return Ok(orders);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                return BadRequest("Order can't be null!");
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


            if (!await _orderService.AddOrder(orderDTO, (int)id))
            {
                return BadRequest("Credentials not valid!");
            }
            return Ok("Order added successfully!");
        }

        #endregion
    }
}