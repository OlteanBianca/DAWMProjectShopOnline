using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class ProductDTO
    {
        #region Properties
        [Required, MaxLength(50)]
        public string ProductName { get; set; } = string.Empty;

        [Required, Range(0, 10000)]
        public double Price { get; set; }
        #endregion
    }
}
