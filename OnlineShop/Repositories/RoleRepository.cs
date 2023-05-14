using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        #region Constructors
        public RoleRepository(ShopDbContext dbContext) : base(dbContext) { }
        #endregion
    }
}
