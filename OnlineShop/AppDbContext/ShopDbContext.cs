using Microsoft.EntityFrameworkCore;
using OnlineShop.Entities;

namespace OnlineShop.AppDbContext
{
    public class ShopDbContext : DbContext
    {
        #region Properties
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderedProduct> OrderedProducts { get; set; }

        public DbSet<Role> Roles { get; set; }
        #endregion

        #region Constructors
        public ShopDbContext() { }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        #endregion
    }
}
