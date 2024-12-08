using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public enum UserRole
    {
        Admin,
        RegisteredCustomer,
        GuestCustomer
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        [MaxLength(250)]
        public string? Address { get; set; }  

        [MaxLength(15)]
        public string? Phone { get; set; } 

        [Required]
        [MaxLength(10)]
        public string Status { get; set; } = "Active";

        public ICollection<Order> Orders { get; set; }
        public Cart Cart { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
