using api_portal.Dto;
using api_portal.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_portal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController(FeedbackService feedbackService) : ControllerBase
    {
        private readonly FeedbackService _feedbackService = feedbackService;

        [HttpPost]
        public IActionResult CreateFeedback([FromBody] FeedbackRequest request)
        {
            if (request == null || request.Rating < 1 || request.Rating > 5)
            {
                return BadRequest("Invalid feedback data.");
            }

            var feedback = _feedbackService.CreateFeedback(request);
            return CreatedAtAction(nameof(GetAllFeedback), new { id = feedback.FeedbackID }, feedback);
        }

        [HttpGet]
        public IActionResult GetAllFeedback()
        {
            var feedbacks = _feedbackService.GetAllFeedback();
            return Ok(feedbacks);
        }

        [HttpGet("book/{bookId}")]
        public IActionResult GetFeedbackByBook(int bookId)
        {
            var feedbacks = _feedbackService.GetFeedbackByBook(bookId);

            if (!feedbacks.Any())
            {
                return NotFound($"No feedback found for book ID {bookId}.");
            }

            return Ok(feedbacks);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetFeedbackByUser(int userId)
        {
            var feedbacks = _feedbackService.GetFeedbackByUser(userId);

            if (!feedbacks.Any())
            {
                return NotFound($"No feedback found for user ID {userId}.");
            }

            return Ok(feedbacks);
        }
    }
}
