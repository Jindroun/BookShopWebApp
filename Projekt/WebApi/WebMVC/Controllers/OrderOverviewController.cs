using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models;

namespace WebMVC.Controllers;
public class OrderOverviewController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOrderService orderService;
    private readonly IUserService userService;
    private readonly IMemoryCache cache;


    public OrderOverviewController(ILogger<HomeController> logger, IOrderService serviceO, IUserService serviceU, IMemoryCache memoryCache)
    {
        _logger = logger;
        orderService = serviceO;
        userService = serviceU;
        cache = memoryCache;
    }

    public async Task<IActionResult> Index()
    {
        var Guid = User.Claims.FirstOrDefault().Value;
        var id = await userService.GetUserIdByGuidAsync(Guid);

        if (id == null)
        {
            return RedirectToAction("Error");
        }

        var orders_query = orderService.FetchOrdersDisplayByUser(id.Value);

        if (orders_query == null)
        {
            return RedirectToAction("Error");
        }

        var orders = await orders_query.ToListAsync();

        var viewModel = new OrderOverviewViewModel
        {
            Orders = orders
        };

        return View(viewModel);
    }




}
