using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Mappings;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(UnitOfWork unitOfWork, IAuthorizationService authService) : base(unitOfWork, authService)
        {
        }

        #region Interface Implementations
        public async Task<bool> AddProduct(ProductDTO productDTO)
        {
            Product product = new()
            {
                Price = productDTO.Price,
                Name = productDTO.ProductName
            };
            await _unitOfWork.Products.Insert(product);
            return await _unitOfWork.SaveChanges();
        }

        public async Task<bool> FindProductByName(string productName)
        {
            if(productName == null)
            {
                return false;
            }
            var result = await _unitOfWork.Products.GetByName(productName);

            return result == null;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            return (await _unitOfWork.Products.GetAll()).ToProductDTOs();
        }

        public async Task<ProductDTO> GetProductByID(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if(product == null)
            {
                return new ProductDTO();
            }
            return product.ToProductDTO();
        }

        public async Task<bool> Remove(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);

            if(product == null)
            {
                return false;
            }

            product.IsDeleted = true;
            _unitOfWork.Products.Update(product);
           return await _unitOfWork.SaveChanges();
        }

        public async Task<bool> Update(int id, double price)
        {
            var product = await _unitOfWork.Products.GetById(id);

            if(product == null)
            {
                return false;
            }

            product.Price = price;
            _unitOfWork.Products.Update(product);
            return await _unitOfWork.SaveChanges();
        }

        #endregion
    }
}
