using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using UnitTests.Mocks;

namespace UnitTests;

public class OrderServiceTests
{
    private readonly MockedDependencyInjectionBuilder mockedDependencyInjectionBuilder;
    private readonly IOrderService orderService;
    private readonly IUserService userService;


    public OrderServiceTests()
    {
        mockedDependencyInjectionBuilder = new MockedDependencyInjectionBuilder()
            .AddMockdDBContext()
            .AddServices()
            .AddMapper();

        var serviceProvider = mockedDependencyInjectionBuilder.Create();

        orderService = serviceProvider.GetRequiredService<IOrderService>();
        userService = Substitute.For<IUserService>();
        var user = GetFakeUser();
        userService.GetUserEntityAsync(Arg.Any<int>()).Returns(user);
    }

    [Fact]
    public void FetchOrders_ShouldReturn()
    {
        // Act
        var orders = orderService.FetchOrders();

        Assert.NotNull(orders);
        Assert.True(orders.Count() >= 1);
    }

    [Fact]
    public async Task OrdersByUser_ShouldReturn()
    {
        // Arrange
        var userId = 1;

        // Act
        var orders = await orderService.OrdersByUserAsync(userId);

        Assert.NotNull(orders);
        Assert.True(orders.IsSuccess);
        Assert.True(orders.Data!.Count() >= 1);
    }

    [Fact]
    public void OrdersByState_ShouldReturn()
    {
        // Arrange
        var state = OrderState.Paid;

        // Act
        var orders = orderService.OrdersByState(state);

        Assert.NotNull(orders);
        Assert.True(orders.Count() >= 1);
    }

    [Fact]
    public async Task PlaceOrderAsync_ShouldReturn()
    {
        // Arrange
        var userId = 1;
        var address = new AddressDto
        {
            Street = "Test Street",
            City = "Test City",
            PostalCode = "12345",
            Country = "Test Country"
        };
        var shopItem = new ShopItemDto
        {
            BookId = 1,
            Stock = 10,
            Price = 10
        };

        var items = new List<OrderItemDto>
        {
            new OrderItemDto
            {
                ShopItemId = 1,
                Count = 1
            }
        };

        // Act
        var order = await orderService.PlaceOrderAsync(userId, address, items);
        Assert.True(order.IsSuccess);

        var orderData = order.Data!;

        Assert.NotNull(order);
        Assert.Equal(userId, orderData.UserId);
        Assert.Equal(address.Street, orderData.Address.Street);
        Assert.Equal(address.City, orderData.Address.City);
        Assert.Equal(address.PostalCode, orderData.Address.PostalCode);
        Assert.Equal(address.Country, orderData.Address.Country);
        Assert.Single(orderData.OrderItems);
    }

    private UserEntity GetFakeUser()
    {
        return new UserEntity
        {
            Id = 1,
            FirstName = "Test",
            LastName = "User",
            Addresses = [ new AddressEntity
            {
                Street = "Main Street",
                City = "New York",
                PostalCode = "10001",
                Country = "USA"
            } ]
        };
    }
}
