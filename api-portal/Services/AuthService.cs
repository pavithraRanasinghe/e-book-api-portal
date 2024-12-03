using api_portal.Data;
using api_portal.Security;
using System.Security.Cryptography;
using System.Text;

namespace api_portal.Services
{
    public class AuthService
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(ApplicationDBContext dbContext, JwtTokenService jwtTokenService)
        {
            _dbContext = dbContext;
            _jwtTokenService = jwtTokenService;
        }

        public string Login(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                return null;
            }

            return _jwtTokenService.GenerateToken(user.Email, user.Role);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            using var sha256 = SHA256.Create();
            var hashedInput = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return hashedPassword == hashedInput;
        }
    }
}
