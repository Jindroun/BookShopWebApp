using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrdersController : Controller
{
    private readonly IOrderService orderService;
    private readonly IShopItemService shopItemService;

    public OrdersController(IOrderService OrderService, IShopItemService ShopItemService)
    {
        orderService = OrderService;
        shopItemService = ShopItemService;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await orderService.GetOrdersAdmin().ToListAsync(); // List of OrderDtos
        var shopItems = await shopItemService.GetShopItems().ToListAsync(); // List of possible shop items


        var viewModel = new OrdersViewModel
        {
            Orders = orders,
            ShopItems = shopItems
        };

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] OrderDto orderDto)
    {

        if (orderDto == null)
        {
            return Json(new { success = false, message = "Invalid data" });
        }

        try
        {
            // Save changes to the database
            await orderService.UpdateOrderAsync(orderDto.Id.Value, orderDto);

            return Json(new { success = true });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = ex.Message });
        }
    }

    // POST: /Admin/Books/Create
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OrderDto orderDto)
    {
        if (orderDto == null)
        {
            return Json(new { success = false, message = "Invalid data." });
        }

        var result = await orderService.PlaceWholeOrderAsync(orderDto);
        if (result.IsSuccess)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false, message = "Error adding new order." });
        }
    }

    // POST: /Admin/Books/Delete/{id}
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await orderService.DeleteOrderAsync(id);
        if (result.IsSuccess)
        {
            return Json(new { success = true });
        }
        else
        {
            return Json(new { success = false, message = "Error deleting the order." });
        }
    }


}
