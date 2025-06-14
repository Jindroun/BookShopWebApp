using BusinessLayer;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopItemController : Controller
{
    private readonly IShopItemService shopItemService;
    private readonly IMemoryCache cache;

    public ShopItemController(IShopItemService shopItemService, IMemoryCache cache)
    {
        this.shopItemService = shopItemService;
        this.cache = cache;
    }

    // Fetch all shop items
    [HttpGet("fetch")]
    public async Task<ActionResult<IEnumerable<ShopItemDisplay>>> Fetch() => await shopItemService.GetShopItems().ToListAsync();

    // Fetch by book
    [HttpGet("fetch/book/{bookId}")]
    public async Task<ActionResult<IEnumerable<ShopItemDisplay>>> FetchByBook(int bookId)
    {
        var cacheKey = CacheKeys.GetShopItemByBookKey(bookId.ToString()!);

        if (!cache.TryGetValue(cacheKey, out IEnumerable<ShopItemDisplay>? cachedShopItems))
        {
            var shopItems = await shopItemService.GetShopItems().Where(si => si.BookId == bookId).ToListAsync();
            cache.Set(cacheKey, shopItems, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30)));
        }

        return Ok(cachedShopItems);
    }

    // Add new shop item
    [HttpPost("add")]
    public async Task<ActionResult<ShopItemDisplay>> Add([FromBody] ShopItemDto shopItemDto)
    {
        var result = await shopItemService.AddShopItem(shopItemDto);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return CreatedAtAction(nameof(Fetch), new { shopItemId = result.Data!.Id }, result.Data);
    }

    // Update shop item
    [HttpPut("update/{shopItemId}")]
    public async Task<ActionResult<ShopItemDisplay>> Update(int shopItemId, [FromBody] ShopItemDto updateRatingDto)
    {
        var result = await shopItemService.UpdateShopItem(shopItemId, updateRatingDto);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return Ok(result.Data);
    }

    // Delete shop item
    [HttpDelete("{shopItemId}")]
    public async Task<ActionResult<ShopItemDisplay>> Delete(int shopItemId, [FromQuery] bool force = false)
    {
        var result = await shopItemService.DeleteShopItem(shopItemId, force);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return Ok("Shop item succesfully deleted.");
    }
}
