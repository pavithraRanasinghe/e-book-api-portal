using api_portal.Data;
using api_portal.Dto;
using api_portal.Model;

namespace api_portal.Services
{
    public class FeedbackService
    {
        private readonly ApplicationDBContext _dbContext;

        public FeedbackService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Feedback CreateFeedback(FeedbackRequest request)
        {
            var feedback = new Feedback
            {
                UserID = request.UserID,
                BookID = request.BookID,
                Rating = request.Rating,
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

        public IEnumerable<Feedback> GetFeedbackByBook(int bookId)
        {
            return _dbContext.Feedbacks.Where(f => f.BookID == bookId).ToList();
        }

        public IEnumerable<Feedback> GetFeedbackByUser(int userId)
        {
            return _dbContext.Feedbacks.Where(f => f.UserID == userId).ToList();
        }
    }
}
