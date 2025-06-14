using BusinessLayer;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using WebMVC.Models;

namespace WebMVC.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IShopItemService shopItemService;
    private readonly IGenreService genreService;
    private readonly IAuthorService authorService;
    private readonly IMemoryCache cache;


    public HomeController(ILogger<HomeController> logger, IShopItemService serviceI, IGenreService serviceG, IAuthorService serviceA, IMemoryCache memoryCache)
    {
        _logger = logger;
        shopItemService = serviceI;
        genreService = serviceG;
        authorService = serviceA;
        cache = memoryCache;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        string cacheKey = CacheKeys.GetShopItemsPageKey(page);
        const int itemsPerPage = 6;

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };

        if (!cache.TryGetValue(cacheKey, out PaginatedResult<ShopItemDisplay>? paginatedResult))
        {
            paginatedResult = await shopItemService.GetShopItemsByPage(page, itemsPerPage);
            cache.Set(cacheKey, paginatedResult, cacheEntryOptions);
        }

        var totalPages = (int)Math.Ceiling(paginatedResult.TotalItemsCount / (double)itemsPerPage);

        var viewModel = new LandingPageViewModel
        {
            ShopItems = paginatedResult.Items,
            TotalPages = totalPages,
            CurrentPage = page
        };

        return View(viewModel);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpGet]
    public async Task<JsonResult> SearchBooks(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Json(new { books = new List<ShopItemDisplay>(), authors = new List<string>(), genres = new List<string>() });
        }
        // Helper function for splitting and normalizing input strings
        Func<string?, IEnumerable<string>> normalizeInput = input =>
            input?.Trim().ToLower().Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Enumerable.Empty<string>();

        var queriesNormalized = normalizeInput(query);
        // Perform the search (case-insensitive) in your database
        var books = await shopItemService.GetShopItems().Where(b => !queriesNormalized.Any() || queriesNormalized.Any(g => b.BookTitle.ToLower().Contains(g)))
            .Take(5) // Limit to 5 results
            .ToListAsync();

        var authors = await authorService.GetAuthors().Where(b => !queriesNormalized.Any() ||
                          queriesNormalized.Any(name => b.FirstName.ToLower().Contains(name)) ||
                          queriesNormalized.Any(name => b.LastName.ToLower().Contains(name)))
            .Take(5) // Limit to 5 results
            .ToListAsync();

        var genres = await genreService.GetGenres().Where(b => !queriesNormalized.Any() || queriesNormalized.Any(g => b.Name.ToLower().Contains(g)))
          .Take(5) // Limit to 5 results
          .ToListAsync();

        return Json(new { books, authors, genres });
    }

}
