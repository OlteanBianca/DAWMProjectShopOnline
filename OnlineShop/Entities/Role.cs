using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Entities
{
    public class Role : BaseEntity
    {
        #region Properties
        [Required, MaxLength(50)]
        public string RoleName { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
        #endregion
    }
}
