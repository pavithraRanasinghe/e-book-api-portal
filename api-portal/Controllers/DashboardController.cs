using api_portal.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController(DashboardService dashboardService) : ControllerBase
    {
        private readonly DashboardService _dashboardService = dashboardService;

        // Fetch total stats for books, users, orders, and revenue
        [HttpGet("stats")]
        public IActionResult GetStats()
        {
            var stats = _dashboardService.GetDashboardStats();
            return Ok(stats);
        }

        // Fetch recent orders
        [HttpGet("recent-orders")]
        public IActionResult GetRecentOrders()
        {
            var recentOrders = _dashboardService.GetRecentOrders();
            return Ok(recentOrders);
        }

        // Fetch chart data (orders and revenue)
        [HttpGet("chart-data")]
        public IActionResult GetChartData()
        {
            var chartData = _dashboardService.GetChartData();
            return Ok(chartData);
        }
    }

}
