using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class InventoryDTO
    {
        #region Properties
        [Required]
        public int ShopId { get; set; }
        [Required]
        public string ProductName { get; set; } = String.Empty;

        [Required, Range(0, 10000)]
        public int Quantity { get; set; }
        #endregion
    }
}
