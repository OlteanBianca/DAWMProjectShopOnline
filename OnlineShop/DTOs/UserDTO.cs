using System.ComponentModel.DataAnnotations;

namespace OnlineShop.DTOs
{
    public class UserDTO
    {
        #region Properties
        [Required]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }
        #endregion
    }
}
