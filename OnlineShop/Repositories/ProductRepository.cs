using Microsoft.EntityFrameworkCore;
using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(ShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Product?> GetByName(string name)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Name == name);
        }
    }
}
