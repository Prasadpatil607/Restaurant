using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailsRepository _detailRepository;
        public DetailsController(IDetailsRepository details)
        {
            _detailRepository = details;
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            var res = await _detailRepository.GetDetails();
            return Ok(res);
        }
    }
}
