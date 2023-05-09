using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Services;
using IAuthorizationService = OnlineShop.Services.IAuthorizationService;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("inventory")]
    [Authorize(Roles = "ShopOwner")]
    public class InventoryController : ControllerBase
    {
        #region Private Fields
        private readonly IInventoryService _inventoryService;
        private readonly IUserService _userService;
        private readonly IAuthorizationService _authorizationService;
        private int _userId;
        #endregion

        #region Constructors
        public InventoryController(IInventoryService inventoryService, IAuthorizationService authorizationService, IUserService userService)
        {
            _inventoryService = inventoryService;
            _authorizationService = authorizationService;
            _userService = userService;
            _userId = -1;
        }
        #endregion

        #region Private Methods
        private string CheckUser()
        {
            string? accessToken = Request.Headers[HeaderNames.Authorization];

            if (accessToken == null)
            {
                return "Access Denied!";
            }

            int? userId = _authorizationService.GetUserFromToken(accessToken);

            if (userId == null)
            {
                return "User not found!";
            }
            _userId = userId.Value;
            return "";
        }
        #endregion

        #region Public Methods
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            string result = CheckUser();

            if(result != "")
            {
                return BadRequest(result);
            }

            return Ok(await _inventoryService.GetAll(_userId));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToInventory([FromBody] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                return BadRequest("Inventory can't be null!");
            }

            string result = CheckUser();

            if (result != "")
            {
                return BadRequest(result);
            }


            if (!await _inventoryService.AddToInventory(inventoryDTO, _userId))
            {
                return BadRequest("Error adding to Inventory!");
            }
            return Ok("Inventory added successfully!");
        }

        [HttpPatch("edit-quantity")]
        public async Task<IActionResult> EditQuantity([FromBody] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                return BadRequest("Inventory can't be null!");
            }

            string result = CheckUser();

            if (result != "")
            {
                return BadRequest(result);
            }

            if (!await _inventoryService.EditQuantity(inventoryDTO, _userId))
            {
                return BadRequest("Error editing the Inventory!");
            }
            return Ok("Inventory edited successfully!");

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> RemoveFromInventory([FromBody] InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                return BadRequest("Inventory can't be null!");
            }

            string result = CheckUser();

            if (result != "")
            {
                return BadRequest(result);
            }

            if (!await _inventoryService.Remove(inventoryDTO, _userId))
            {
                return BadRequest("Error removing from the Inventory!");
            }
            return Ok("Inventory updated successfully!");
        }
        #endregion
    }
}
