using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Handlers;
using RestaurantDemo.Models;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly INavItemsHandler _navItemsHandler;
        private readonly ISectionsHandler _sectionsHandler;
        private readonly IMenuHandler _menuHandler;
        private readonly IFeedbackHandler _feedbackHandler;
        private readonly IDetailsHandler _detailsHandler;
        private readonly IContactUsHandler _contactUsHandler;
        private readonly IServiceHoursHandler _serviceHoursHandler;

        public HomeController(INavItemsHandler navItemsHandler, ISectionsHandler sectionsHandler, IMenuHandler menuHandler, IFeedbackHandler feedbackHandler, IDetailsHandler detailsHandler, IContactUsHandler contactUsHandler, IServiceHoursHandler serviceHoursHandler)
        {
            _navItemsHandler = navItemsHandler;
            _sectionsHandler = sectionsHandler;
            _menuHandler = menuHandler;
            _feedbackHandler = feedbackHandler;
            _detailsHandler = detailsHandler;
            _contactUsHandler = contactUsHandler;
            _serviceHoursHandler = serviceHoursHandler;
        }   

        [HttpGet("NavItems")]
        [ProducesResponseType(typeof(NavItems), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<List<NavItems>>> GetNavItems()
        {
            var navItems = await _navItemsHandler.GetNavItems();
            if (navItems == null)
            {
                return StatusCode(404, "NavItems not found");
            }
            return Ok(navItems);
        }

        [HttpGet("Sections")]
        [ProducesResponseType(typeof(Sections), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<List<Sections>>> GetSections()
        {
            var sections = await _sectionsHandler.GetSections();
            if (sections == null)
            {
                return StatusCode(404, "Sections not found");
            }
            return Ok(sections);
        }

        [HttpGet("Menu")]
        [ProducesResponseType(typeof(Menu), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Menu>>> GetMenu()
        {
            var res = await _menuHandler.GetMenus();
            if(res == null)
            {
                return StatusCode(404, "Menu not found");
            }
            return Ok(res);
        }

        [HttpGet("Feedback")]
        [ProducesResponseType(typeof(Feedback), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<List<Feedback>>> GetFeedbacks()
        {
            var feedbacks = await _feedbackHandler.GetFeedbacks();
            if(feedbacks == null)
            {
                return StatusCode(404, "feedbacks not found");
            }
            return Ok(feedbacks);
        }

        [HttpGet("Details")]
        [ProducesResponseType(typeof(Details), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Details>>> GetDetails()
        {
            var details = await _detailsHandler.GetDetails();
            if(details == null)
            {
                return StatusCode(404, "Details not found");
            }
            return Ok(details);
        }

        [HttpGet("ContactUs")]
        [ProducesResponseType(typeof(ContactUs), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<List<ContactUs>>> GetContactUs()
        {
            var contactUs = await _contactUsHandler.GetContacts();
            if(contactUs == null)
            {
                return StatusCode(404, "Contacts not found");
            }
            return Ok(contactUs);
        }

        [HttpGet("ServiceHours")]
        [ProducesResponseType(typeof(ServiceHours), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<ActionResult<List<ServiceHours>>> GetServiceHours()
        {
            var serviceHours = await _serviceHoursHandler.GetServiceHours();
            if(serviceHours == null)
            {
                return StatusCode(404, "Not found");
            }
            return Ok(serviceHours);
        }

        

    }
}
