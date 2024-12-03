using api_portal.Data;
using api_portal.Dto;
using api_portal.Model;
using System.Security.Cryptography;
using System.Text;

namespace api_portal.Services
{
    public class CustomerService
    {
        private readonly ApplicationDBContext _dbContext;

        public CustomerService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Register(CustomerRequest request)
        {
            if (_dbContext.Users.Any(u => u.Email == request.Email))
            {
                return false; // Email already exists
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = HashPassword(request.Password),
                Role = "Customer",
                Address = request.Address,
                Phone = request.Phone
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return true;
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        public IEnumerable<User> FindAll()
        {
            return _dbContext.Users.Where(u => u.Role == "Customer").ToList();
        }

        public User FindById(int id)
        {
            return _dbContext.Users.SingleOrDefault(u => u.UserID == id && u.Role == "Customer");
        }
    }
}
