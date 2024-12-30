namespace api_portal.Dto.Response
{
    public class RecentOrder
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
    }
}
