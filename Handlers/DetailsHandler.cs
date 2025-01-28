using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface IDetailsHandler
    {
        Task<List<Details>> GetDetails();
    }
    public class DetailsHandler : IDetailsHandler
    {
        private readonly IRestaurantRepository _detailsRepository;
        public DetailsHandler(IRestaurantRepository detailsRepository)
        {
            _detailsRepository = detailsRepository;
        }

        public async Task<List<Details>> GetDetails()
        {
            return await _detailsRepository.GetDetails();
        }
    }
}
