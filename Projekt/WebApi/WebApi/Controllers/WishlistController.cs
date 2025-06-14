using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WishlistController : Controller
{
    private readonly IWishlistService wishlistService;

    public WishlistController(IWishlistService service)
    {
        wishlistService = service;
    }

    // Fetch all wishlists
    [HttpGet("fetch")]
    public async Task<ActionResult<IEnumerable<RatingEntity>>> Fetch()
    {
        var wishlistQuery = wishlistService.GetWishlistEntries();

        var wishlists = await wishlistQuery
            .GroupBy(w => w.UserId)
            .ToListAsync();

        return Ok(wishlists);
    }
    
    // Fetch wishlist entries by user
    [HttpGet("{userId}")]
    public async Task<ActionResult<IEnumerable<RatingEntity>>> FetchByUser(int userId)
    {

        var wishlistQuery = wishlistService.GetWishlistEntries();

        var wishlists = await wishlistQuery
            .Where(w => w.UserId == userId)
            .GroupBy(w => w.UserId)
            .ToListAsync();

        return Ok(wishlists);

    }
    
    // Delete a wishlist item
    [HttpDelete("{wishlistItemId}")]
    public async Task<ActionResult> DeleteRating(int wishlistEntryId)
    {

        var result = await wishlistService.DeleteWishlistEntry(wishlistEntryId);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.CustomErrorMessage });
        }

        return Ok("Wishlist entry succesfully deleted.");

    }
    
    // Action method for adding a new wishlistItem
    [HttpPost("add")]
    public async Task<ActionResult> AddWishlistItem([FromBody] WishlistEntryDto newWishlistEntryDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await wishlistService.AddWishlistEntry(newWishlistEntryDto);

        if (!result.IsSuccess)
        {
            return NotFound(new { Message = result.CustomErrorMessage });
        }

        return CreatedAtAction(nameof(AddWishlistItem), new { ratingId = result.Data.Id }, result.Data);
    }
   
}

