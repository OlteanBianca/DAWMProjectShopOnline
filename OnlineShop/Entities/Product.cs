using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class Product : BaseEntity
    {
        #region Properties
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Inventory> Inventories { get; } = new List<Inventory>();
        public ICollection<OrderedProduct> OrderedProducts { get; } = new List<OrderedProduct>();
        #endregion
    }
}
