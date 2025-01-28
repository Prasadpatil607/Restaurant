using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface IFeedbackHandler
    {
        Task<List<Feedback>> GetFeedbacks();
    }
    public class FeedbackHandler : IFeedbackHandler
    {
        private readonly IRestaurantRepository _feedbacks;
        public FeedbackHandler(IRestaurantRepository feedbackRepository)
        {
            _feedbacks = feedbackRepository;
        }

        public async Task<List<Feedback>> GetFeedbacks()
        {
            return await _feedbacks.GetFeedbacks();
        }
    }
}
