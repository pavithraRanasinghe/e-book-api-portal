using api_portal.Data;
using api_portal.Model;
using Microsoft.EntityFrameworkCore;

namespace api_portal.Util
{
    public class PreLoader
    {
        private readonly ApplicationDBContext _context;

        public PreLoader(ApplicationDBContext context)
        {
            _context = context;
        }

        public void SeedInitialData()
        {
            _context.Database.Migrate();

            AddInitialUsers();
        }


        private void AddInitialUsers()
        {
            // Check if any users exist
            if (!_context.Users.Any())
            {
                if (!_context.Users.Any(u => u.Role == UserRole.Admin.ToString() && u.Email == "EsoftAdmin"))
                {
                    // Add Admin user
                    var adminUser = new User
                    {
                        Name = "Esoft Admin",
                        Email = "admin@esoft.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("EsoftAdmin"),
                        Phone = "1234567890",
                        Role = UserRole.Admin.ToString(),
                        Status = "Active"
                    };
                    _context.Users.Add(adminUser);
                }

                if (!_context.Users.Any(u => u.Role == UserRole.RegisteredCustomer.ToString() && u.Email == "EsoftCustomer"))
                {

                    // Add Customer user
                    var customerUser = new User
                    {
                        Name = "Esoft Customer",
                        Email = "customer@esoft.com",
                        Phone = "1234567890",
                        Password = BCrypt.Net.BCrypt.HashPassword("EsoftCustomer"),
                        Role = UserRole.RegisteredCustomer.ToString(),
                        Status = "Active"
                    };
                    _context.Users.Add(customerUser);
                }
                _context.SaveChanges();
            }
        }
    }
}
