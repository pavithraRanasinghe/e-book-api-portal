using api_portal.Data;
using api_portal.Dto;
using api_portal.Model;

namespace api_portal.Services
{
    public class CartService
    {
        private readonly ApplicationDBContext _dbContext;

        public CartService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cart AddToCart(CartRequest request)
        {
            var cart = _dbContext.Carts.SingleOrDefault(c => c.UserID == request.UserID);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserID = request.UserID,
                    CreatedDate = System.DateTime.UtcNow
                };
                _dbContext.Carts.Add(cart);
                _dbContext.SaveChanges();
            }

            var cartItem = _dbContext.CartItems.SingleOrDefault(ci => ci.CartID == cart.CartID && ci.BookID == request.BookID);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    CartID = cart.CartID,
                    BookID = request.BookID,
                    Quantity = request.Quantity
                };
                _dbContext.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += request.Quantity;
            }

            _dbContext.SaveChanges();

            return cart;
        }

        public Cart GetCartByUserId(int userId)
        {
            return _dbContext.Carts
                .Where(c => c.UserID == userId)
                .Select(c => new Cart
                {
                    CartID = c.CartID,
                    UserID = c.UserID,
                    CreatedDate = c.CreatedDate,
                    CartItems = _dbContext.CartItems
                        .Where(ci => ci.CartID == c.CartID)
                        .Select(ci => new CartItem
                        {
                            CartItemID = ci.CartItemID,
                            CartID = ci.CartID,
                            BookID = ci.BookID,
                            Quantity = ci.Quantity,
                            Book = _dbContext.Books.SingleOrDefault(b => b.BookID == ci.BookID)
                        }).ToList()
                }).SingleOrDefault();
        }
    }
}
