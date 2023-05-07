using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("order")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        #region Private Fields
        private readonly IOrderService _orderService;
        #endregion

        #region Constructors
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
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
        #endregion
    }
}