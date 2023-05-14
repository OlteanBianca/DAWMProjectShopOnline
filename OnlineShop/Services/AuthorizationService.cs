using Microsoft.IdentityModel.Tokens;
using OnlineShop.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OnlineShop.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        #region Private Fields
        private readonly string _securityKey;
        private readonly int PBKDF2IterCount = 1000;
        private readonly int PBKDF2SubkeyLength = 256 / 8;
        private readonly int SaltSize = 128 / 8;
        #endregion

        #region Constructors
        public AuthorizationService(IConfiguration config)
        {
            _securityKey = config["JWT:SecurityKey"] ?? string.Empty;
        }
        #endregion

        #region Private Methods
        private static bool ByteArraysEqual(byte[] a, byte[] b)
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

        private bool ValidateToken(string tokenString)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_securityKey));

            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = false,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
            };

            if (!jwtTokenHandler.CanReadToken(tokenString.Replace("Bearer ", string.Empty)))
            {
                Console.WriteLine("Invalid Token");
                return false;
            }

            jwtTokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);
            return validatedToken != null;
        }
        #endregion

        #region Public Methods
        public string GetToken(User user, Role role)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_securityKey));
            SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

            Claim roleClaim = new("role", role.RoleName);
            Claim idClaim = new("userId", user.Id.ToString());
            Claim infoClaim = new("username", user.Email);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, idClaim, infoClaim }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = credentials
            };
            SecurityToken token = jwtTokenHandler.CreateToken(tokenDescriptor);

            string bearerToken = jwtTokenHandler.WriteToken(token);
            if (!ValidateToken(bearerToken))
            {
                return "";
            }
            return bearerToken;
        }

        public int? GetUserFromToken(string token)
        {
            JwtSecurityTokenHandler jwtTokenHandler = new();
            JwtSecurityToken readToken = jwtTokenHandler.ReadJwtToken(token.Replace("Bearer ", string.Empty));

            Claim? claim = readToken.Claims.FirstOrDefault(claim => claim.Type == "userId");

            if (claim == null)
            {
                return null;
            }
            return Convert.ToInt32(claim.Value);
        }

        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            byte[] salt;
            byte[] subkey;
            using (Rfc2898DeriveBytes deriveBytes = new(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            byte[] outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            byte[] storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (Rfc2898DeriveBytes deriveBytes = new(password, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }
        #endregion
    }
}
