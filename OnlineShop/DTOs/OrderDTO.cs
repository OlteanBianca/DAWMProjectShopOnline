using OnlineShop.Entities;
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

        public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
        #endregion
    }
}
