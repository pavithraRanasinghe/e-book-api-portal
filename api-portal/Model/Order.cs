using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
    }
}
