using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;
using System.Collections.Generic;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NavItemsController : ControllerBase
    {
        private readonly INavItemsRepository _navItemsRepository;

        public NavItemsController(INavItemsRepository navItemsRepository)
        {
            _navItemsRepository = navItemsRepository;
        }

        [HttpGet]
        public ActionResult<List<NavItems>> GetNavItems()
        {
            var navItems = _navItemsRepository.GetNavItems();
            return Ok(navItems);
        }
    }
}
