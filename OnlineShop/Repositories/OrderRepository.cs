using Microsoft.EntityFrameworkCore;
using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        #region Constructors
        public OrderRepository(ShopDbContext dbContext) : base(dbContext) { }
        #endregion

        #region Public Methods

        public async Task<List<Order>> GetByUserId(int userId)
        {
            return await _dbContext.Orders.Where(order => order.UserId == userId).ToListAsync();
        }
        #endregion
    }
}
