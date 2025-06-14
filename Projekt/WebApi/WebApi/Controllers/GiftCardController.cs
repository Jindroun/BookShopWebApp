using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GiftCardController : ControllerBase
{
    private readonly IGiftCardService giftCardService;

    public GiftCardController(IGiftCardService service)
    {
        giftCardService = service;
    }

    // Fetch all gift cards
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GiftCardDisplay>>> Fetch()
    {
        var giftCards = await giftCardService.GetGiftCardsAsync();
        return Ok(giftCards);
    }
    
    // Fetch gift card by id
    [HttpGet("{id}")]
    public async Task<ActionResult<GiftCardDisplay>> FetchById(int id)
    {

        var result = await giftCardService.GetGiftCardAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }
        return Ok(result.Data);
    }
    
    // Add a gift card
    [HttpPost]
    public async Task<ActionResult<GiftCardDisplay>> AddGiftCard([FromBody] GiftCardDto giftCardDto)
    {

        var result = await giftCardService.AddGiftCardAsync(giftCardDto);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return CreatedAtAction(nameof(FetchById), new { id = result.Data!.Id }, result.Data);
    }
    
    // Delete a gift card
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGiftCard(int id)
    {

        var result = await giftCardService.DeleteGiftCardAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return Ok();
    }
}
