using OnlineShop.AppDbContext;

namespace OnlineShop.Repositories
{
    public class UnitOfWork
    {
        #region Private Fields
        private readonly ShopDbContext _dbContext;
        #endregion

        #region Properties
        public RoleRepository Roles { get; }
        public UserRepository Users { get; }
        public ShopRepository Shops { get; }
        public ProductRepository Products { get; }
        #endregion

        #region Constructors
        public UnitOfWork(ShopDbContext dbContext, 
                          RoleRepository roleRepository, 
                          UserRepository userRepository,
                          ProductRepository productRepository,
                          ShopRepository shopRepository)
        {
            _dbContext = dbContext;
            Roles = roleRepository;
            Users = userRepository;
            Products = productRepository;
            Shops = shopRepository;
        }
        #endregion

        #region Public Methods
        public async Task<bool> SaveChanges()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
                return false;
            }
            return true;
        }
        #endregion
    }
}
