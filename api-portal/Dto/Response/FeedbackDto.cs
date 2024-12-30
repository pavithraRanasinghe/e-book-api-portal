namespace api_portal.Dto.Response
{
    public class FeedbackDto
    {
        public int FeedbackID { get; set; }
        public int BookID { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }

}
