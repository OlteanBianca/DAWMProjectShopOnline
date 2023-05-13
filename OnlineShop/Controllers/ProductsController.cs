using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.DTOs;
using OnlineShop.Services;

namespace OnlineShop.Controllers
{
    [ApiController]
    [Route("products")]
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
            var isUnique = await _productService.FindProductByName(payload.ProductName);

            if (!isUnique)
            {
                return BadRequest("Name cannot be null and must be unique!!");
            }

            var result = await _productService.AddProduct(payload);

            if (!result)
            {
                return BadRequest("Product couldn't be added!");
            }

            return Ok(result);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<ProductDTO>>> GetAll(int pageNumber = 1, int pageSize = 2, bool isDescending = false)
        {
            var productsList = await _productService.GetAllProducts();
            int totalItems = productsList.Count();

            if (isDescending)
            {
                productsList = productsList.Reverse();
            }
          

            // Calculate the total number of pages needed.
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Use the current page number and number of items per page to determine which items to display.
            int startIndex = (pageNumber - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize - 1, totalItems - 1);

            // Add the appropriate pagination parameters to your API endpoint.
            var paginationHeader = new
            {
                pageNumber,
                pageSize,
                totalPages,
                totalItems
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationHeader));

            // Update your API endpoint logic to retrieve the appropriate subset of data based on the pagination parameters.
            var subset = productsList.Skip(startIndex).Take(endIndex - startIndex + 1).ToList();

            return Ok(subset);
         
        }

        [HttpGet("get-by-id/{productId}")]
        public async Task<ActionResult<ProductDTO>> GetById(int productId)
        {
            var result = await _productService.GetProductByID(productId);

            return Ok(result);
        }

        [HttpPatch("edit-price/{productId}")]
        public async Task<ActionResult<bool>> GetById(int productId, [FromBody] double price)
        {
            var result = await _productService.Update(productId, price);

            if (!result)
            {
                return BadRequest("Price could not be updated.");
            }

            return result;
        }

        [HttpDelete("delete-by-id/{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var result = await _productService.Remove(productId);

            if(!result)
            {
                return BadRequest("Product couldn't be deleted!");
            }
            return Ok();
        }
        #endregion
    }
}
