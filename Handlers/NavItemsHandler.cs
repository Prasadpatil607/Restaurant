using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface INavItemsHandler
    {
        Task<List<NavItems>> GetNavItems();
    }
    public class NavItemsHandler : INavItemsHandler
    {
        private readonly IRestaurantRepository _navItemsRepository;
        public NavItemsHandler(IRestaurantRepository navItemsRepository)
        {
            _navItemsRepository = navItemsRepository;
        }

        public async Task<List<NavItems>> GetNavItems()
        {
            return await _navItemsRepository.GetNavItems();
        }
    }
}
