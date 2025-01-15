using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        public readonly IContactUsRepository _contactUsRepository;
        public ContactUsController(IContactUsRepository contact) {
            _contactUsRepository = contact;
        }

        [HttpPost]
        public ActionResult AddContact([FromBody]ContactUs contact)
        {
            if(contact == null)
            {
                return BadRequest("Invalid");
            }
            var res = _contactUsRepository.AddContact(contact);
            return Ok(res);
        }

        [HttpGet]
        public ActionResult<List<ContactUs>> GetAll()
        {
            var contacts = _contactUsRepository.GetContacts();
            return Ok(contacts);
        }
    }
}
