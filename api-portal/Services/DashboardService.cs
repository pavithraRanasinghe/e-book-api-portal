using api_portal.Data;
using api_portal.Dto.Response;
using System;

namespace api_portal.Services
{
    public class DashboardService
    {
        private readonly ApplicationDBContext _dbContext;

        public DashboardService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DashboardStats GetDashboardStats()
        {
            var booksCount = _dbContext.Books.Count();
            var usersCount = _dbContext.Users.Count();
            var ordersCount = _dbContext.Orders.Count();
            var totalRevenue = _dbContext.Orders.Sum(o => o.TotalAmount);

            return new DashboardStats
            {
                Books = booksCount,
                Users = usersCount,
                Orders = ordersCount,
                Revenue = totalRevenue
            };
        }

        public List<RecentOrder> GetRecentOrders()
        {
            return _dbContext.Orders
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .Select(o => new RecentOrder
                {
                    Id = o.OrderID,
                    Customer = o.User.Name,
                    Date = o.OrderDate.ToString("yyyy-MM-dd"),
                    Status = o.OrderStatus.ToString(),
                    Total = o.TotalAmount
                })
                .ToList();
        }

        public ChartData GetChartData()
        {
            var chartData = new ChartData
            {
                Labels = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" },
                Datasets = new List<ChartDataset>
            {
                new ChartDataset
                {
                    Label = "Orders",
                    Data = new List<int> { 12, 19, 8, 15, 10, 22, 18 },
                    BackgroundColor = "rgba(75, 192, 192, 0.6)"
                },
                new ChartDataset
                {
                    Label = "Revenue ($)",
                    Data = new List<int> { 200, 300, 150, 400, 250, 500, 350 },
                    BackgroundColor = "rgba(153, 102, 255, 0.6)"
                }
            }
            };

            return chartData;
        }
    }

}
