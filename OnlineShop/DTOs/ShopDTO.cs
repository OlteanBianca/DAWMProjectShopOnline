using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class ShopDTO
    {
        #region Properties
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Address { get; set; } = null!;
        #endregion
    }
}
