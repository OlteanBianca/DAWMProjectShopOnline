using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class Shop : BaseEntity
    {
        #region Properties
        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Address { get; set; } = null!;

        public User User { get; set; } = null!;
        public ICollection<Inventory> Inventories { get; } = new List<Inventory>();
        public ICollection<OrderedProduct> OrderedProducts { get; } = new List<OrderedProduct>();
        #endregion
    }
}
