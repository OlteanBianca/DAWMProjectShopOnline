using OnlineShop.DTOs;

namespace OnlineShop.Services
{
    public interface IUserService
    {
        #region Public Methods
        public Task<LoginDTO?> Register(RegisterDTO registerData);

        public Task<string> Validate(LoginDTO payload);

        public Task<UserDTO?> GetUserById(int id);

        public Task<UserDTO?> FindUserByEmail(string email);

        public Task<bool> UpdateUserToShopOwner(int id);
        #endregion
    }
}
