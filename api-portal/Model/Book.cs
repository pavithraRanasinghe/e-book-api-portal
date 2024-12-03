using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [MaxLength(13)]
        public string ISBN { get; set; }

        public DateTime PublishedDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}
