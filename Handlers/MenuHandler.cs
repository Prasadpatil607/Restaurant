using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Handlers
{
    public interface IMenuHandler
    {
        Task<List<Menu>> GetMenus();
    }
    public class MenuHandler : IMenuHandler
    {
        private readonly IRestaurantRepository _menuRepository;
        public MenuHandler(IRestaurantRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<Menu>> GetMenus()
        {
            return await _menuRepository.GetMenu();
        }
    }
}
