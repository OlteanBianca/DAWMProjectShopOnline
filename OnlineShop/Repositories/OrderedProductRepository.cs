using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class OrderedProductRepository : BaseRepository<OrderedProduct>
    {
        #region Constructors
        public OrderedProductRepository(ShopDbContext dbContext) : base(dbContext) { }
        #endregion
    }
}
