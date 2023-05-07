using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class ProductDTOMapping
    {
        #region Entity To DTO

        public static List<ProductDTO> ToProductDTOs(this List<Product> products)
        {
            var results = products.Select(e => e.ToProductDTO()).ToList();

            if (results.Any())
            {
                return results;
            }
            return new List<ProductDTO>();
        }

        public static ProductDTO ToProductDTO(this Product product)
        {
            if (product == null) return new ProductDTO();

            ProductDTO productDTO = new()
            {
                ProductName = product.Name,
                Price = product.Price
            };

            return productDTO;
        }
        #endregion
    }
}
