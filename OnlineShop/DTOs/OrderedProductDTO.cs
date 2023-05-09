using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class OrderedProductDTO
    {
        #region Properties
        [Required, MaxLength(30)]
        public string ShopName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }
        #endregion
    }
}
