using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class InventoryDTO
    {
        #region Properties
        [Required]
        public string ShopName { get; set; } = string.Empty;

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Required, Range(0, 10000)]
        public int Quantity { get; set; }
        #endregion
    }
}
