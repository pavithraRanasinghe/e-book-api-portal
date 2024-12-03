namespace api_portal.Dto
{
    public class FeedbackRequest
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}
