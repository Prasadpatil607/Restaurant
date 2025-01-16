using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Details()
        {
            var res = _detailRepository.GetDetails();
            return Ok(res);
        }
    }
}
