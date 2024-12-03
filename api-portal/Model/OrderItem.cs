using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }

        public int Quantity { get; set; }

        public decimal Subtotal { get; set; }
    }
}
