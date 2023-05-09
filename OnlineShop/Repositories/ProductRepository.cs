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

        #region Public Methods
        public async Task<Product?> GetByName(string name)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(product => product.Name == name);
        }
    
        public new async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.Where(s => s.IsDeleted == false).ToListAsync();
        }

        public new async Task<Product?> GetById(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(s => s.Id == id);

            if(product == null)
            {
                return null;
            }

            if(product.IsDeleted == true)
            {
                return null;
            }

            return product;
        }
        #endregion
    }
}
