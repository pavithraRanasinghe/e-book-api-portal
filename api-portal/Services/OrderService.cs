﻿using api_portal.Data;
using api_portal.Dto;
using api_portal.Dto.Response;
using api_portal.Model;
using Microsoft.EntityFrameworkCore;

namespace api_portal.Services
{
    public class OrderService
    {
        private readonly ApplicationDBContext _dbContext;

        public OrderService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order CheckoutCart(CheckoutRequest request)
        {
            var cart = _dbContext.Carts.SingleOrDefault(c => c.UserID == request.UserID);

            if (cart == null || !_dbContext.CartItems.Any(ci => ci.CartID == cart.CartID))
            {
                return null; // Cart is empty or doesn't exist
            }

            var cartItems = _dbContext.CartItems.Where(ci => ci.CartID == cart.CartID).ToList();

            var totalAmount = cartItems.Sum(ci =>
            {
                var book = _dbContext.Books.SingleOrDefault(b => b.BookID == ci.BookID);
                return book != null ? book.Price * ci.Quantity : 0;
            });

            var order = new Order
            {
                UserID = request.UserID,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderStatus = "Pending"
            };

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            var orderItems = cartItems.Select(ci =>
            {
                var book = _dbContext.Books.SingleOrDefault(b => b.BookID == ci.BookID);

                if (book == null) return null;

                // Decrease stock
                book.StockQuantity -= ci.Quantity;

                return new OrderItem
                {
                    OrderID = order.OrderID,
                    BookID = ci.BookID,
                    Quantity = ci.Quantity,
                    Subtotal = book.Price * ci.Quantity
                };
            }).Where(oi => oi != null).ToList();

            _dbContext.OrderItems.AddRange(orderItems);

            // Add payment record
            var payment = new Payment
            {
                OrderID = order.OrderID,
                PaymentDate = DateTime.UtcNow,
                PaymentMethod = request.PaymentMethod,
                PaymentStatus = "Paid",
                Amount = totalAmount
            };

            _dbContext.Payments.Add(payment);

            // Clear cart
            _dbContext.CartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();

            return order;
        }

        public bool UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = _dbContext.Orders.SingleOrDefault(o => o.OrderID == orderId);

            if (order == null)
            {
                return false;
            }

            order.OrderStatus = newStatus;
            _dbContext.SaveChanges();
            return true;
        }


        public IEnumerable<OrderDto> FindAllOrders()
        {
            var orders = _dbContext.Orders
                .Include(o => o.OrderItems) 
                    .ThenInclude(oi => oi.Book)
                .Select(order => new OrderDto
                {
                    OrderID = order.OrderID,
                    UserID = order.UserID,
                    Customer = order.User.Name,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.OrderStatus,
                    OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                    {
                        OrderItemID = oi.OrderItemID,
                        BookID = oi.BookID,
                        Quantity = oi.Quantity,
                        Subtotal = oi.Subtotal,
                        Book = new BookDto
                        {
                            BookID = oi.Book.BookID,
                            Title = oi.Book.Title,
                            Author = oi.Book.Author,
                            Price = oi.Book.Price
                        }
                    }).ToList()
                })
                .ToList();

            return orders;
        }


        public IEnumerable<Order> FindOrdersByUserId(int userId)
        {
            return _dbContext.Orders
                .Where(order => order.UserID == userId)
                .Select(order => new Order
                {
                    OrderID = order.OrderID,
                    UserID = order.UserID,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.OrderStatus,
                    OrderItems = _dbContext.OrderItems
                        .Where(oi => oi.OrderID == order.OrderID)
                        .Select(oi => new OrderItem
                        {
                            OrderItemID = oi.OrderItemID,
                            BookID = oi.BookID,
                            Quantity = oi.Quantity,
                            Subtotal = oi.Subtotal,
                            Book = _dbContext.Books.SingleOrDefault(b => b.BookID == oi.BookID)
                        }).ToList()
                }).ToList();
        }

        public IEnumerable<Order> FindOrdersByStatus(string status)
        {
            return _dbContext.Orders
                .Where(order => order.OrderStatus.ToLower() == status.ToLower())
                .Select(order => new Order
                {
                    OrderID = order.OrderID,
                    UserID = order.UserID,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = order.OrderStatus,
                    OrderItems = _dbContext.OrderItems
                        .Where(oi => oi.OrderID == order.OrderID)
                        .Select(oi => new OrderItem
                        {
                            OrderItemID = oi.OrderItemID,
                            BookID = oi.BookID,
                            Quantity = oi.Quantity,
                            Subtotal = oi.Subtotal,
                            Book = _dbContext.Books.SingleOrDefault(b => b.BookID == oi.BookID)
                        }).ToList()
                }).ToList();
        }

        public bool RemoveOrderById(int orderId)
        {
            var order = _dbContext.Orders.SingleOrDefault(o => o.OrderID == orderId);

            if (order == null)
            {
                return false; // Order not found
            }

            // Remove related order items
            var orderItems = _dbContext.OrderItems.Where(oi => oi.OrderID == orderId).ToList();
            _dbContext.OrderItems.RemoveRange(orderItems);

            // Remove payment record
            var payment = _dbContext.Payments.SingleOrDefault(p => p.OrderID == orderId);
            if (payment != null)
            {
                _dbContext.Payments.Remove(payment);
            }

            // Remove the order
            _dbContext.Orders.Remove(order);

            _dbContext.SaveChanges();
            return true;
        }

    }
}
