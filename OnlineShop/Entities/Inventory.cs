using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class Inventory : BaseEntity
    {
        #region Properties

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ShopId { get; set; }

        public Product Product { get; set; } = null!;

        public Shop Shop { get; set; } = null!;
        #endregion
    }
}
