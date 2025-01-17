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
        public async Task<IActionResult> AddContact([FromBody] ContactUs contact)
        {
            if (contact == null)
            {
                return BadRequest("Invalid");
            }
            var res = await _contactUsRepository.AddContact(contact);
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactUs>>> GetAll()
        {
            var contacts = await _contactUsRepository.GetContacts();
            return Ok(contacts);
        }

        //[HttpPut("{id}")]
        //public IActionResult UpdateContact(int id, [FromBody] ContactUs contactUs)
        //{
        //    if (contactUs == null || id != contactUs.Id)
        //    {
        //        return BadRequest("Contact ID mismatch");
        //    }

        //    try
        //    {
        //        var result = _contactUsRepository.UpdateContact(contactUs);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


        //[HttpDelete("{id}")]
        //public IActionResult DeleteContact(int id)
        //{
        //    try
        //    {
        //        var result = _contactUsRepository.DeleteContact(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

    }
}
