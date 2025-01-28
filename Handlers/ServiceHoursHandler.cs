using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface IServiceHoursHandler
    {
        Task<List<ServiceHours>> GetServiceHours();
    }
    public class ServiceHoursHandler : IServiceHoursHandler
    {
        private readonly IRestaurantRepository _serviceHoursRepository;
        public ServiceHoursHandler(IRestaurantRepository serviceHoursRepository)
        {
            _serviceHoursRepository = serviceHoursRepository;
        }

        public async Task<List<ServiceHours>> GetServiceHours()
        {
            return await _serviceHoursRepository.GetServiceHours();
        }
    }
}
