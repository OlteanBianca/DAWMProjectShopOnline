using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OnlineShop.DTOs;
using OnlineShop.Services;
using IAuthorizationService = OnlineShop.Services.IAuthorizationService;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("inventory")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        #region Private Fields
        private readonly IInventoryService _inventoryService;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public InventoryController(IInventoryService inventoryService, IAuthorizationService authorizationService, IUserService userService)
        {
            _inventoryService = inventoryService;
            _authorizationService = authorizationService;
        }
        #endregion

        #region Public Methods
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            string? accessToken = Request.Headers[HeaderNames.Authorization];

            if (accessToken == null)
            {
                return BadRequest("Access Denied!");
            }

            int? userId = _authorizationService.GetUserFromToken(accessToken);

            if (userId == null)
            {
                return NotFound("User not found!");
            }
            return Ok(await _inventoryService.GetAll(userId.Value));
        }

        [HttpPost("add")]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> AddToInventory([FromBody] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                return BadRequest("Inventory can't be null!");
            }

            if (!await _inventoryService.AddToInventory(inventoryDTO))
            {
                return BadRequest("Error adding to Inventory!");
            }
            return Ok("Inventory added successfully!");
        }

        [HttpPatch("edit-quantity")]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> EditQuantity([FromBody] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                return BadRequest("Inventory can't be null!");
            }

            if (!await _inventoryService.EditQuantity(inventoryDTO))
            {
                return BadRequest("Error editing the Inventory!");
            }
            return Ok("Inventory edited successfully!");
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "ShopOwner")]
        public async Task<IActionResult> RemoveFromInventory([FromBody] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                return BadRequest("Inventory can't be null!");
            }

            string? accessToken = Request.Headers[HeaderNames.Authorization];

            if (accessToken == null)
            {
                return BadRequest("Access Denied!");
            }

            int? userId = _authorizationService.GetUserFromToken(accessToken);

            if (userId == null)
            {
                return NotFound("User not found!");
            }

            if (!await _inventoryService.Remove(inventoryDTO))
            {
                return BadRequest("Error removing from the Inventory!");
            }
            return Ok("Inventory updated successfully!");
        }
        #endregion
    }
}
