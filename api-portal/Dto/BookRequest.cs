namespace api_portal.Dto
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
