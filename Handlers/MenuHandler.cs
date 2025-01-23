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
        private readonly IMenuRepository _menuRepository;
        public MenuHandler(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public async Task<List<Menu>> GetMenus()
        {
            return await _menuRepository.GetMenu();
        }
    }
}
