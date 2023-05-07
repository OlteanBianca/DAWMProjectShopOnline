using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class UserDTOMapping
    {
        #region DTO to Entity
        public static User? ToUser(this UserDTO userDTO)
        {
            if (userDTO == null) return null;

            User user = new()
            {
                RoleId = userDTO.RoleId,
                Name = userDTO.Name,
                Address = userDTO.Address,
                PhoneNumber = userDTO.PhoneNumber,
                DateOfBirth = userDTO.DateOfBirth,
                Email = userDTO.Email,
            };

            return user;
        }
        #endregion

        #region Entity To DTO
        public static UserDTO? ToUserDTO(this User user)
        {
            if (user == null) return null;

            UserDTO userDTO = new()
            {
                RoleId = user.RoleId,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
            };

            return userDTO;
        }
        #endregion
    }
}
