using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DTOs;
using OnlineShop.Services;
using System.Security.Claims;

namespace OnlineShop.Controllers
{
        [ApiController]
        [Route("api/orders")]
        [Authorize]
        public class OrdersController : ControllerBase
        {
            private readonly IOrderService _orderService;

            public OrdersController(IOrderService orderService)
            {
                _orderService = orderService;
            }

            //[HttpPost("add")]
            //[Authorize(Roles = "Client")]
            //public async Task<IActionResult> Add(List<AddOrderDTO> payload)
            //{
            //    var result = await _orderService.Create(payload);

            //    if (!result)
            //    {
            //        return BadRequest("Class cannot be added");
            //    }

            //    return Ok(result);
            //}

            //[HttpGet("get-all")]
            //public async Task<ActionResult<List<ClassViewDto>>> GetAll()
            //{
            //    var result = await _classService.GetAll();

            //    return Ok(result);
            //}

            //[HttpGet("get-by-id")]
            //public async Task<ActionResult<ClassViewDto>> GetById([FromBody] int id)
            //{
            //    var result = await _classService.Get(id);

            //    return Ok(result);
            //}
        }
    }

