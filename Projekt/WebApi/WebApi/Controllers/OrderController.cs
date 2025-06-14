using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly IUserService userService;

    public OrderController(IOrderService orderService, IUserService userService)
    {
        this.orderService = orderService;
        this.userService = userService;
    }

    // Fetch all orders
    [HttpGet("fetch")]
    public async Task<ActionResult<IEnumerable<OrderEntity>>> Fetch() => await orderService.FetchOrders().ToListAsync();

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDisplay>> FetchById(int orderId)
    {
        var result = await orderService.GetOrderAsync(orderId);

        if (!result.IsSuccess)
        {
            return BadRequest(new { Message = result.CustomErrorMessage });
        }

        return Ok(result.Data);
    }

    // Fetch orders by user
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<OrderEntity>>> FetchByUser(int userId)
    {
        var orders = await orderService.OrdersByUserAsync(userId);

        if (orders == null)
        {
            return NotFound("No orders found");
        }

        return Ok(orders);
    }

    // Fetch orders by state
    [HttpGet("state/{state}")]
    public async Task<ActionResult<IEnumerable<OrderEntity>>> FetchByState(OrderState state) => await orderService.OrdersByState(state).ToListAsync();

    // place a new order
    [HttpPost("place")]
    public async Task<ActionResult<OrderEntity>> Place([FromBody] OrderDto orderDto)
    {
        var user = await userService.GetUserAsync(orderDto.UserId);
        if (user == null)
        {
            return BadRequest("User not found");
        }

        var userAddress = new AddressDto
        {
            Street = user.Street,
            City = user.City,
            PostalCode = user.PostalCode,
            Country = user.Country
        };

        var address = orderDto.Address ?? userAddress;

        var order = await orderService.PlaceWholeOrderAsync(orderDto);
        if (!order.IsSuccess)
        {
            return BadRequest(order.CustomErrorMessage);
        }

        return Ok(order.Data);
    }

    // Delete order
    [HttpDelete("{orderId}")]
    public async Task<ActionResult<OrderEntity>> Delete(int orderId)
    {
        var order = await orderService.DeleteOrderAsync(orderId);

        if (!order.IsSuccess)
        {
            return NotFound(new { Message = order.CustomErrorMessage });
        }

        return Ok(order);
    }
}
