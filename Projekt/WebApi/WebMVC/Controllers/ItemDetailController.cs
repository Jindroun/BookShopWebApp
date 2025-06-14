using BusinessLayer;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class ItemDetailController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IShopItemService shopItemService;
    private readonly IMemoryCache cache;

    public ItemDetailController(ILogger<HomeController> logger, IShopItemService serviceI, IMemoryCache memoryCache)
    {
        _logger = logger;
        shopItemService = serviceI;
        cache = memoryCache;
    }

    public async Task<ActionResult> Index(int id)
    {
        string cacheKey = CacheKeys.GetShopItemKey(id);

        if (!cache.TryGetValue(cacheKey, out ShopItemDisplay? item))
        {
            item = await shopItemService.GetShopItems()
                .FirstOrDefaultAsync(i => i.Id == id);
            cache.Set(cacheKey, item, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30)));
        }

        if (item == null)
        {
            return RedirectToAction("Error");
        }

        var viewModel = new ItemDetailViewModel
        {
            shopItem = item,
        };

        return View(viewModel);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
