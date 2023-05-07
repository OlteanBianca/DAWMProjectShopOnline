using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(ShopDbContext dbContext) : base(dbContext)
        {
        }
    }
}
