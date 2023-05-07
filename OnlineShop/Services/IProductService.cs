using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public interface IProductService
    {
        public Task<bool> AddProduct(ProductDTO productDTO);
        public Task<IEnumerable<ProductDTO>> GetAllProducts();
        public Task<bool> Update(ProductDTO productToUpdate);
        public Task<bool> Remove(int id);
        public Task<ProductDTO> GetProductByID(int id);
    }
}
