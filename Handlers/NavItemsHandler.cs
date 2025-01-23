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
        private readonly INavItemsRepository _navItemsRepository;
        public NavItemsHandler(INavItemsRepository navItemsRepository)
        {
            _navItemsRepository = navItemsRepository;
        }

        public async Task<List<NavItems>> GetNavItems()
        {
            return await _navItemsRepository.GetNavItems();
        }
    }
}
