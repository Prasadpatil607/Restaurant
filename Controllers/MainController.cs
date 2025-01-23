using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Handlers;
using RestaurantDemo.Models;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INavItemsHandler _navItemsHandler;
        private readonly ISectionsHandler _sectionsHandler;
        private readonly IMenuHandler _menuHandler;
        private readonly IFeedbackHandler _feedbackHandler;
        private readonly IDetailsHandler _detailsHandler;
        private readonly IContactUsHandler _contactUsHandler;
        private readonly IServiceHoursHandler _serviceHoursHandler;

        public MainController(INavItemsHandler navItemsHandler, ISectionsHandler sectionsHandler, IMenuHandler menuHandler, IFeedbackHandler feedbackHandler, IDetailsHandler detailsHandler, IContactUsHandler contactUsHandler, IServiceHoursHandler serviceHoursHandler)
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
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<NavItems>>> GetNavItems()
        {
            var navItems = await _navItemsHandler.GetNavItems();
            return Ok(navItems);
        }

        [HttpGet("Sections")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Sections>>> GetSections()
        {
            var sections = await _sectionsHandler.GetSections();
            return Ok(sections);
        }

        [HttpGet("Menu")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Menu>>> GetMenu()
        {
            var res = await _menuHandler.GetMenus();
            return Ok(res);
        }

        [HttpGet("Feedback")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Feedback>>> GetFeedbacks()
        {
            var feedbacks = await _feedbackHandler.GetFeedbacks();
            return Ok(feedbacks);
        }

        [HttpGet("Details")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Details>>> GetDetails()
        {
            var details = await _detailsHandler.GetDetails();
            return Ok(details);
        }

        [HttpGet("ContactUs")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ContactUs>>> GetContactUs()
        {
            var contactUs = await _contactUsHandler.GetContacts();
            return Ok(contactUs);
        }

        [HttpGet("ServiceHours")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ServiceHours>>> GetServiceHours()
        {
            var serviceHours = await _serviceHoursHandler.GetServiceHours();
            return Ok(serviceHours);
        }

        

    }
}
