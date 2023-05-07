using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public partial class OrderedProduct : BaseEntity
    {
        #region Properties
        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? ShopId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Order? Order { get; set; } = null!;
        public Product? Product { get; set; } = null!;
        public Shop? Shops { get; set; } = null!;
        #endregion
    }
}
