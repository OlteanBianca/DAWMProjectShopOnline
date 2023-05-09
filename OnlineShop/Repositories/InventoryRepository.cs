using Microsoft.EntityFrameworkCore;
using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>
    {
        #region Constructors
        public InventoryRepository(ShopDbContext dbContext) : base(dbContext){   }
        #endregion

        #region Public Methods
        public async Task<List<Inventory>> GetAll(int userId)
        {
           return await _dbContext.Inventories
                .Include(e => e.Product)
                .Include(e => e.Shop)            
                .ThenInclude(e => e.User)
                .Where(e => e.Shop.UserId == userId)
                .ToListAsync();
        }

        public async Task<Inventory?> GetByShopIdAndProductId(int shopId, int productId)
        {
            return await _dbContext.Inventories.FirstAsync(e => e.ShopId == shopId && e.ProductId == productId);
        }
        #endregion
    }
}
