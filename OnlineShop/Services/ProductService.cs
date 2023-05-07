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

        public async Task<bool> AddProduct(ProductDTO productDTO)
        {
            Product product = new()
            {
                Price = productDTO.Price,
                Name = productDTO.ProductName
            };
            product = await _unitOfWork.Products.Insert(product);
            await _unitOfWork.SaveChanges();
            return product != null;
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

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ProductDTO productToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
