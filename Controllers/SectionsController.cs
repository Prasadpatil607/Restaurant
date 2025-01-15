using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly ISectionRepository sectionRepository;
        public SectionsController(ISectionRepository repository) {
             sectionRepository= repository;
        }

        [HttpGet]
        public ActionResult<List<Sections>> getSections()
        {
            var sections = sectionRepository.GetSections();
            return Ok(sections);
        }

        [HttpPost]
        public ActionResult AddSection([FromBody] Sections section)
        {
            if (section == null) 
                return BadRequest("Invalid Section");

            var sec = sectionRepository.AddSection(section);
            return Ok(sec);
        }

        [HttpPut]
        public ActionResult UpdateSection(int id, [FromBody] Sections section)
        {
            if (section == null)
                return BadRequest("Section is not present");

            if (id != section.Id)
                return BadRequest("Section id is not matching");

            try
            {
                sectionRepository.UpdateSection(section);
                return NoContent();
            }
            catch (Exception ex)
            {   
                return BadRequest(ex.Message);
            }
        }
    }
}
