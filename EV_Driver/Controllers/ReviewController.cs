using BusinessObject.Dtos;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace EV_Driver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController(IReviewService reviewService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await reviewService.GetAllAsync());

        [HttpGet("station/{stationId}")]
        public async Task<IActionResult> GetByStation(string stationId) =>
            Ok(await reviewService.GetByStationAsync(stationId));

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(string userId) =>
            Ok(await reviewService.GetByUserAsync(userId));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var review = await reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ReviewRequest review)
        {
            await reviewService.AddAsync(review);
            return Ok(new { message = "Review added successfully" });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ReviewRequest review)
        {
            await reviewService.UpdateAsync(review);
            return Ok(new { message = "Review updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await reviewService.DeleteAsync(id);
            return Ok(new { message = "Review deleted successfully" });
        }
    }
}
