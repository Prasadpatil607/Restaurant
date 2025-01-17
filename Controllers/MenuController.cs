using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
           this.menuRepository = menuRepository;
        }

        [HttpGet]        
        public async Task<ActionResult<List<Menu>>> GetMenu()
        {
            var menu = await menuRepository.GetMenu();
            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult> AddMenuItem([FromBody]Menu menu)
        {
            if (menu == null)
                return BadRequest("Invalid menu item");

            var item = await menuRepository.AddMenuItem(menu);
            return Ok(item);
        }

        //[HttpPut]
        //public ActionResult EditMenuItem([FromBody] Menu menu)
        //{
        //    if (menu == null)
        //        return BadRequest("Menu item not found");
        //    var res=menuRepository.UpdateMenuItem(menu);
        //    return Ok(res);
        //}


        //[HttpDelete]
        //public ActionResult DeleteMenuItem(int id) { 
        //    var result = menuRepository.DeleteMenuItem(id);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    else
        //        return BadRequest("Menu item not found!");
            
        //}
    }
}
