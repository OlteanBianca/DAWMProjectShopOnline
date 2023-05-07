using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShop.AppDbContext;
using OnlineShop.Entities;

namespace OnlineShop.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        #region Private Fields
        private readonly DbSet<T> _dbSet;
        #endregion

        #region Public Fields
        public readonly ShopDbContext _dbContext;
        #endregion

        #region Constructors
        public BaseRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        #endregion

        #region Protected Methods
        protected IQueryable<T> GetRecords()
        {
            return _dbSet.AsQueryable<T>();
        }
        #endregion

        #region Public Methods
        public async Task<List<T>> GetAll()
        {
            return await GetRecords().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<T> Insert(T entity)
        {
            EntityEntry<T> newEntity = await _dbSet.AddAsync(entity);
            return newEntity.Entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public bool Any(Func<T, bool> expression)
        {
            return GetRecords().Any(expression);
        }
        #endregion
    }
}
