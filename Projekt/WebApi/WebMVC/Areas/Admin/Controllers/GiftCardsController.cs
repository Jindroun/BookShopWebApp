using AutoMapper;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Models;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class GiftCardsController : Controller
{
    private readonly IGiftCardService giftCardService;
    private readonly ICouponCodeService couponCodeService;
    private readonly IMapper mapper;

    public GiftCardsController(IGiftCardService GiftCardService, ICouponCodeService CouponCodeService, IMapper Mapper)
    {
        giftCardService = GiftCardService;
        couponCodeService = CouponCodeService;
        mapper = Mapper;
    }

    public async Task<IActionResult> Index()
    {
        var giftCards = await giftCardService.GetGiftCardsAsync();
        var viewModel = new GiftCardsViewModel
        {
            GiftCards = giftCards
        };
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult AddGiftCard()
    {
        return View(new AddGiftCardViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> AddGiftCard(AddGiftCardViewModel model)
    {
        if (ModelState.IsValid)
        {
            await giftCardService.AddGiftCardAsync(mapper.Map<GiftCardDto>(model));
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult AddCouponCode(int? giftCardId = null)
    {
        var model = new AddCouponCodeViewModel();
        if (giftCardId.HasValue)
        {
            model.GiftCardId = giftCardId.Value;
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddCouponCode(AddCouponCodeViewModel model)
    {
        if (ModelState.IsValid)
        {
            await couponCodeService.AddCouponCodeAsync(mapper.Map<CouponCodeDto>(model));
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteGiftCard(int id)
    {
        await giftCardService.DeleteGiftCardAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCouponCode(int id)
    {
        await couponCodeService.DeleteCouponCodeAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
