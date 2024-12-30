using api_portal.Data;
using api_portal.Dto;
using api_portal.Dto.Response;
using api_portal.Model;
using Microsoft.EntityFrameworkCore;

namespace api_portal.Services
{
    public class FeedbackService
    {
        private readonly ApplicationDBContext _dbContext;
        private AuthService _authService;

        public FeedbackService(ApplicationDBContext dbContext, AuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public Feedback CreateFeedback(FeedbackRequest request)
        {
            var feedback = new Feedback
            {
                UserID = request.UserID,
                BookID = request.BookID,
                Comments = request.Comments,
                FeedbackDate = System.DateTime.UtcNow
            };

            _dbContext.Feedbacks.Add(feedback);
            _dbContext.SaveChanges();

            return feedback;
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _dbContext.Feedbacks.ToList();
        }

        public IEnumerable<FeedbackDto> GetFeedbackByBook(int bookId)
        {
            var feedbackList = _dbContext.Feedbacks
                .Where(f => f.BookID == bookId)
                .Select(f => new FeedbackDto
                {
                    FeedbackID = f.FeedbackID,
                    BookID = f.BookID,
                    Comment = f.Comments,
                    Date = f.FeedbackDate,
                    UserName = f.User.Name
                })
                .ToList();

            return feedbackList;
        }


        public IEnumerable<Feedback> GetFeedbackByUser(int userId)
        {
            return _dbContext.Feedbacks.Where(f => f.UserID == userId).ToList();
        }
    }
}
