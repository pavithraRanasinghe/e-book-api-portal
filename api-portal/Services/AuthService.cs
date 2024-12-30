using api_portal.Data;
using api_portal.Dto.Response;
using api_portal.Model;
using api_portal.Security;
using BCrypt.Net;  // Import BCrypt.Net

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

        // Login method
        public LoginResponse Login(string email, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Email == email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            // Return JWT token
            var token = _jwtTokenService.GenerateToken(user.Email, user.Role);


            return new LoginResponse
            {
                Id = user.UserID,
                Name = user.Name,
                Role = user.Role,
                Token = token,
            };
        }

        // Password verification using BCrypt
        private static bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify password using BCrypt
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        // Find user by email to check if they exist
        public User FindByEmail(string email)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Email == email);
        }

        // Find user by id to check if they exist
        public User FindById(int id)
        {
            return _dbContext.Users.SingleOrDefault(u => u.UserID == id);
        }
    }
}
