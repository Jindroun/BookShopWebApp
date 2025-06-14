using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingsController : Controller
    {
        private  IRatingService ratingService;

        public RatingsController(IRatingService service)
        {
            ratingService = service;
        }
        // Fetch all ratings
        [HttpGet("fetch")]
        public async Task<ActionResult<IEnumerable<RatingEntity>>> Fetch()
        {
            var ratingsQuery = ratingService.GetRatings();
            var ratings = await ratingsQuery.ToListAsync();
            return Ok(ratings);
        }
        // Fetch ratings by user
        [HttpGet("fetchByAttribute")]
        public async Task<ActionResult<IEnumerable<RatingEntity>>> FetchByAttributes
            ([FromQuery] int? userId, [FromQuery] int? bookId)
        {
            var ratingsQuery = ratingService.GetRatings();

            var ratingsQueryFiltered = ratingsQuery
                 .Where(r =>
                     (!userId.HasValue || r.UserId == userId) &&
                     (!bookId.HasValue || r.BookId == bookId)
                 );

            var ratings = await ratingsQueryFiltered.ToListAsync();
            return Ok(ratings);
        }

        // Delete a rating
        [HttpDelete("{ratingId}")]
        public async Task<ActionResult> DeleteRating(int ratingId)
        {
            var result = await ratingService.DeleteRating(ratingId);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return Ok("Rating succesfully deleted.");
        }
    
        // Action method for adding a new rating
        [HttpPost("add")]
        public async Task<ActionResult> AddRating([FromBody] RatingDto ratingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await ratingService.AddRating(ratingDto);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return CreatedAtAction(nameof(AddRating), new { ratingId = result.Data.Id }, result.Data);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateRating(int id, [FromBody] RatingDto updateRatingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if the model state is invalid
            }

            var result = await ratingService.UpdateRating(id, updateRatingDto);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return Ok(result.Data);
        }

    }

}
