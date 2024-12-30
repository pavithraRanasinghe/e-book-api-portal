namespace api_portal.Dto.Response
{
    public class OrderItemDto
    {
        public int OrderItemID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public BookDto Book { get; set; }
    }
}
