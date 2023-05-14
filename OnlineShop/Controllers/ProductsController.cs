using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.DTOs;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("product")]
    [Authorize(Roles = "ShopOwner")]
    public class ProductsController : ControllerBase
    {
        #region Private Fields
        private readonly IProductService _productService;
        #endregion

        #region Constructors
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Public Methods
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProductDTO payload)
        {
            if (!await _productService.FindProductByName(payload.ProductName))
            {
                return BadRequest("Name cannot be null and must be unique!!");
            }
            if (!await _productService.AddProduct(payload))
            {
                return BadRequest("Product couldn't be added!");
            }

            return Ok("Product added successfully!");
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll(int pageNumber = 1, int pageSize = 2, bool isDescending = false)
        {
            List<ProductDTO> productsList = await _productService.GetAllProducts();
            int totalItems = productsList.Count;

            if (isDescending)
            {
                productsList.Reverse();
            }

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            var paginationHeader = new
            {
                pageNumber,
                pageSize,
                totalPages,
                totalItems
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            List<ProductDTO> subset = productsList.Skip(startIndex).Take(endIndex - startIndex + 1).ToList();

            return Ok(subset);
        }

        [HttpGet("get-by-id/{productId}")]
        public async Task<ActionResult<ProductDTO>> GetById(int productId)
        {
            return Ok(await _productService.GetProductByID(productId));
        }

        [HttpPatch("edit-price/{productId}")]
        public async Task<ActionResult<bool>> GetById(int productId, [FromBody] double price)
        {
            if (!await _productService.Update(productId, price))
            {
                return BadRequest("Price could not be updated.");
            }
            return Ok("Price updated successfully!");
        }

        [HttpDelete("delete-by-id/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!await _productService.Remove(productId))
            {
                return BadRequest("Product couldn't be deleted!");
            }
            return Ok("Product deleted successfully!");
        }
        #endregion
    }
}
