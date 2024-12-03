using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartItemID { get; set; }

        public int CartID { get; set; }
        public Cart Cart { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }
    }
}
