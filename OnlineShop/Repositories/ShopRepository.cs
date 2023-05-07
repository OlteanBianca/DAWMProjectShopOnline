using Microsoft.EntityFrameworkCore;
using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class ShopRepository : BaseRepository<Shop>
    {
        #region Constructors
        public ShopRepository(ShopDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Methods

        public async Task<bool> CheckIfShopNameExists(string name)
        {
            return await _dbContext.Shops.FirstOrDefaultAsync(shop => shop.Name == name) != null;
        }

        #endregion
    }
}
