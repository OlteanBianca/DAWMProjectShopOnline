using OnlineShop.Entities;

namespace OnlineShop.Services
{
    public interface IAuthorizationService
    {
        #region Public Methods
        public static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        public string GetToken(User user, Role role);

        public bool ValidateToken(string tokenString);

        public string HashPassword(string password);

        public bool VerifyHashedPassword(string hashedPassword, string password);

        public int? GetUserFromToken(string token);
        #endregion
    }
}
