using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Mappings
{
    public static class RegisterDTOMapping
    {
        #region DTO to Entity
        public static User? ToUser(this RegisterDTO registerData)
        {
            if (registerData == null) return null;

            User user = new()
            {
                Email = registerData.Email,
                RoleId = registerData.RoleId,
                Password = registerData.Password,
                Address = registerData.Address,
                DateOfBirth = registerData.DateOfBirth,
                Name = registerData.Name,
                PhoneNumber = registerData.PhoneNumber,
            };

            return user;
        }

        public static LoginDTO? ToLoginDTO(this RegisterDTO registerData)
        {
            if (registerData == null) return null;

            LoginDTO loginDTO = new()
            {
                Email = registerData.Email,
                Password = registerData.Password,
            };

            return loginDTO;
        }
        #endregion
    }
}
