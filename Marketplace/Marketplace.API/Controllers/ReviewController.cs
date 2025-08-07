using Marketplace.Core.DTOs;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService _service)
        {
            this._service = _service;
        }
        #region Get Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                return Ok(await _service.GetReviews(skip, take));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Review>> GetReviewById([FromRoute] string id)
        {
            try
            {
                return Ok(await _service.GetReviewById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Post Methods
        [HttpPost("create-review")]
        public async Task<ActionResult<Review>> CreateReview([FromBody] ReviewDTO reviewDto)
        {
            try
            {
                var review = new Review()
                {
                    Rating = reviewDto.Rating,
                    Text = reviewDto.Text
                };
                return Ok(await _service.AddReview(review));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Put Methods
        [HttpPut("update-review/{id}")]
        public async Task<ActionResult<Review>> UpdateReview([FromRoute] string id, [FromBody] ReviewDTO reviewDto)
        {
            try
            {
                var review = new Review()
                {
                    Id = id,
                    Rating = reviewDto.Rating,
                    Text = reviewDto.Text
                };
                return Ok(await _service.UpdateReview(id, review));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Delete Methods
        [HttpDelete("delete-review/{id}")]
        public async Task<ActionResult<Review>> DeleteReview([FromRoute] string id)
        {
            try
            {
                await _service.DeleteReview(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
