using OnlineShop.DTOs;
using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public interface IUserService
    {
        #region Public Methods
        public Task<User?> AddUser(RegisterDTO registerData);

        public Task<LoginDTO?> Register(RegisterDTO registerData);

        public Task<string> Validate(LoginDTO payload);

        public Task<UserDTO?> GetUserById(int id);

        public Task<UserDTO?> FindUserByEmail(string email);

        public Task<bool> IsRoleValid(int id);

        public Task<bool> UpdateUserToShopOwner(int id);

        #endregion
    }
}
