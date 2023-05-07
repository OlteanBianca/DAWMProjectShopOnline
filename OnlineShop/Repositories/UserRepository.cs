using OnlineShop.AppDbContext;
using OnlineShop.Entities;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        #region Constructors
        public UserRepository(ShopDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Public Methods
        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(s => s.Email == email);
        }
        #endregion
    }
}
