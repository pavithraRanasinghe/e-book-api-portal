using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_portal.Model
{
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedbackID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public int BookID { get; set; }
        public Book Book { get; set; }

        public DateTime FeedbackDate { get; set; }

        public int Rating { get; set; }

        [MaxLength(500)]
        public string Comments { get; set; }
    }
}
