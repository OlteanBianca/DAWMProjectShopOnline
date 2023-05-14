using Microsoft.EntityFrameworkCore;
using OnlineShop.AppDbContext;
using OnlineShop.DTOs;
using OnlineShop.Entities;
using OnlineShop.Mappings;

namespace OnlineShop.Repositories
{
    public class InventoryRepository : BaseRepository<Inventory>
    {
        #region Constructors
        public InventoryRepository(ShopDbContext dbContext) : base(dbContext) { }
        #endregion

        #region Public Methods
        public async Task<Dictionary<string, List<InventoryDTO>>> GetAll(int userId)
        {
            return await _dbContext.Inventories
                 .Include(e => e.Product)
                 .Include(e => e.Shop)
                 .ThenInclude(e => e.User)
                 .Where(e => e.Shop.UserId == userId)
                 .GroupBy(e => e.Shop.Name)
                 .Select(e => new { ShopName = e.Key, Products = e.ToList().ToInventoryDTOs() })
                .ToDictionaryAsync(e => e.ShopName, e => e.Products);
        }

        public async Task<Inventory?> GetByShopIdAndProductId(int shopId, int productId)
        {
            return await _dbContext.Inventories.FirstOrDefaultAsync(e => e.ShopId == shopId && e.ProductId == productId);
        }

        public async Task<bool> IsQuantityInInventory(int shopId, int productId, int quantity)
        {
            Inventory? inventory = await _dbContext.Inventories.FirstOrDefaultAsync(inventory => inventory.ShopId == shopId && inventory.ProductId == productId);
            if (inventory == null)
            {
                return false;
            }
            return inventory.Quantity >= quantity;
        }
        #endregion
    }
}
