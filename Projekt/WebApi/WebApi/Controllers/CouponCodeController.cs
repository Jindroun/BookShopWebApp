using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CouponCodeController : ControllerBase
{
    private readonly ICouponCodeService couponCodeService;

    public CouponCodeController(ICouponCodeService service)
    {
        couponCodeService = service;
    }
    
    // Fetch coupon code by id
    [HttpGet("{id}")]
    public async Task<ActionResult<CouponCodeDisplay>> FetchById(int id)
    {

        var result = await couponCodeService.GetCouponCodeAsync(id);
        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }
        return Ok(result.Data);
    }

    // Fetch coupon code by code
    [HttpGet("code/{code}")]
    public async Task<ActionResult<CouponCodeDisplay>> FetchByCode(string code)
    {

        var result = await couponCodeService.GetCouponCodeAsync(code);
        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }
        return Ok(result.Data);
    }
    
    // Add a coupon code
    [HttpPost]
    public async Task<ActionResult<CouponCodeDisplay>> AddCouponCode([FromBody] CouponCodeDto couponCodeDto)
    {

        var result = await couponCodeService.AddCouponCodeAsync(couponCodeDto);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return CreatedAtAction(nameof(FetchById), new { id = result.Data!.Id }, result.Data);
    }
    
    // Delete a coupon code
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCouponCode(int id)
    {

        var result = await couponCodeService.DeleteCouponCodeAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return Ok();
    }
}
