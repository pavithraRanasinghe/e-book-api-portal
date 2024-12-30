namespace api_portal.Dto.Response
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public string Customer {  get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
