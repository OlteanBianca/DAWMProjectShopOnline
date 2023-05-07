using OnlineShop.AppDbContext;

namespace AuthorizationApp.Repositories
{
    public class UnitOfWork
    {
        #region Private Fields
        private readonly ShopDbContext _dbContext;
        #endregion

        #region Properties
        //public StudentRepository Students { get; }
        //public ClassRepository Classes { get; }
        //public RoleRepository Roles { get; }
        //public UserRepository Users { get; }
        //public GradeRepository Grades { get; }
        #endregion

        #region Constructors
        public UnitOfWork(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
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
