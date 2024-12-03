using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
