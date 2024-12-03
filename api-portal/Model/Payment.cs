using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public DateTime PaymentDate { get; set; }

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [MaxLength(50)]
        public string PaymentStatus { get; set; }

        public decimal Amount { get; set; }
    }
}
