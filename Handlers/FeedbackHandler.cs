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
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackHandler(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<List<Feedback>> GetFeedbacks()
        {
            return await _feedbackRepository.GetFeedbacks();
        }
    }
}
