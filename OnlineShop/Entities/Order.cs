using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class Order : BaseEntity
    {
        #region Properties
        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public double TotalAmount { get; set; } = 0;

        public User User { get; set; } = null!;
        public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
        #endregion
    }
}
