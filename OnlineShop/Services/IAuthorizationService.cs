using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public interface IAuthorizationService
    {
        #region Public Methods
        public string GetToken(User user, Role role);

        public string HashPassword(string password);

        public bool VerifyHashedPassword(string hashedPassword, string password);

        public int? GetUserFromToken(string token);
        #endregion
    }
}
