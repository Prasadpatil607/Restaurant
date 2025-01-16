using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceHoursController : ControllerBase
    {
        private readonly IServiceHourRepository serviceHourRepository;
        public ServiceHoursController(IServiceHourRepository serviceHour) {
            serviceHourRepository = serviceHour;
        }

        [HttpGet]   
        public ActionResult<List<ServiceHours>> GetServiceHour()
        {
            var res = serviceHourRepository.GetServiceHours();
            return Ok(res);
        }
    }
}
