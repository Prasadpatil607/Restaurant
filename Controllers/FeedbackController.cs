using Microsoft.AspNetCore.Mvc;
using RestaurantDemo.Models;
using RestaurantDemo.Repositories;

namespace RestaurantDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;
        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback([FromBody]Feedback feedback)
        {
            if (feedback == null)
            {
                return BadRequest("Invalid format");
            }
            var res = await _feedbackRepository.AddFeedback(feedback);
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<List<Feedback>>> GetFeedbacks()
        {
            var result = await _feedbackRepository.GetFeedbacks();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFeedback(int id)
        {
            try
            {
                var result = _feedbackRepository.DeleteFeedback(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
