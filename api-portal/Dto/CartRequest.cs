namespace api_portal.Dto
{
    public class CartRequest
    {
        public int UserID { get; set; }
        public int BookID { get; set; }
        public int Quantity { get; set; }
    }
}
