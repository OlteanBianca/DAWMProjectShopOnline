using OnlineShop.DTOs;
using OnlineShop.Mappings;
using OnlineShop.Entities;
using OnlineShop.Repositories;

namespace OnlineShop.Services
{
    public class UserService : BaseService, IUserService
    {
        #region Constructors
        public UserService(UnitOfWork unitOfWork, IAuthorizationService authService) : base(unitOfWork, authService)
        {
        }
        #endregion

        #region Public Methods
        public async Task<User?> AddUser(RegisterDTO registerData)
        {
            User? user = registerData.ToUser();
            if (user == null)
            {
                return null;
            }

            var hashedPassword = _authService.HashPassword(registerData.Password);
            user.Password = hashedPassword;

            user = await _unitOfWork.Users.Insert(user);
            await _unitOfWork.SaveChanges();
            return user;
        }

        public async Task<LoginDTO?> Register(RegisterDTO registerData)
        {
            if (registerData == null || !await IsRoleValid(registerData.RoleId))
            {
                return null;
            }

            User? user = await AddUser(registerData);
            if (user == null)
            {
                return null;
            }

            return registerData.ToLoginDTO();
        }

        public async Task<string> Validate(LoginDTO payload)
        {
            User? user = await _unitOfWork.Users.GetByEmail(payload.Email);

            if (user != null && _authService.VerifyHashedPassword(user.Password, payload.Password))
            {
                Role? role = await _unitOfWork.Roles.GetById(user.RoleId);

                if (role != null)
                {
                    return _authService.GetToken(user, role);
                }
            }
            return string.Empty;
        }

        public async Task<UserDTO?> FindUserByEmail(string email)
        {
            return (await _unitOfWork.Users.GetByEmail(email))?.ToUserDTO();
        }

        public async Task<UserDTO?> GetUserById(int id)
        {
            User? user = await _unitOfWork.Users.GetById(id);
            return user?.ToUserDTO();
        }

        public async Task<bool> IsRoleValid(int id)
        {
            Role? role = await _unitOfWork.Roles.GetById(id);
            return role != null;
        }

        public async Task<bool> UpdateUserToShopOwner(int id)
        {
            User? user = await _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return false;
            }
            user.RoleId = 2;

            _unitOfWork.Users.Update(user);
            return await _unitOfWork.SaveChanges();
        }
        #endregion
    }
}
