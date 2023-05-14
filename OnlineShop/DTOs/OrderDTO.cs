using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class OrderDTO
    {
        #region Properties
        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public double TotalAmount { get; set; } = 0;

        public ICollection<OrderedProductDTO> OrderedProducts { get; set; } = new List<OrderedProductDTO>();
        #endregion
    }
}
