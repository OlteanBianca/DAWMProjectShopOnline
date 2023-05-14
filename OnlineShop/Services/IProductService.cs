using OnlineShop.DTOs;

namespace OnlineShop.Services
{
    public interface IProductService
    {
        #region Public Methods
        public Task<bool> AddProduct(ProductDTO productDTO);
        public Task<List<ProductDTO>> GetAllProducts();
        public Task<bool> Update(int id, double price);
        public Task<bool> Remove(int id);
        public Task<ProductDTO> GetProductByID(int id);
        public Task<bool> FindProductByName(string productName);
        #endregion
    }
}
