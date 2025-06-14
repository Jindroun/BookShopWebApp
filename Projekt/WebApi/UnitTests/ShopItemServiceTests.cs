using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using UnitTests.Mocks;

namespace UnitTests;

public class ShopItemServiceTests
{
    private readonly MockedDependencyInjectionBuilder mockedDependencyInjectionBuilder;
    private readonly IShopItemService shopItemService;
    private readonly IUserService userService;


    public ShopItemServiceTests()
    {
        mockedDependencyInjectionBuilder = new MockedDependencyInjectionBuilder()
            .AddMockdDBContext()
            .AddServices()
            .AddMapper();

        var serviceProvider = mockedDependencyInjectionBuilder.Create();

        shopItemService = serviceProvider.GetRequiredService<IShopItemService>();
        userService = Substitute.For<IUserService>();

    }

    [Fact]
    public void GetShopItems_ShouldReturn()
    {
        // Act
        var shopitems = shopItemService.GetShopItems();

        Assert.NotNull(shopitems);
        Assert.True(shopitems.Count() >= 1);
    }

    [Fact]
    public async Task AddShopItem_ShouldReturn()
    {
        // Arrange
        var shopItem = new ShopItemDto { BookId = 1, Price = 15, Stock = 100 };

        // Act
        var result = await shopItemService.AddShopItem(shopItem);
        Assert.True(result.IsSuccess);

        var shopItemData = result.Data!;

        Assert.NotNull(result);
        Assert.NotNull(shopItemData);
        Assert.Equal(shopItem.BookId, shopItemData.BookId);
        Assert.Equal(shopItem.Price, shopItemData.Price);
        Assert.Equal(shopItem.Stock, shopItemData.Stock);
    }

    [Fact]
    public async Task UpdateShopItem_ShouldReturn()
    {
        // Arrange
        var shopItem = new ShopItemDto { BookId = 1, Price = 15, Stock = 100 };
        var shopItemUpdate = new ShopItemDto { BookId = 2, Price = 30, Stock = 10};

        // Act
        var result = await shopItemService.AddShopItem(shopItem);
        Assert.True(result.IsSuccess);

        var shopItemData = result.Data!;
        var shopItemId = result.Data.Id;

        var updResult = await shopItemService.UpdateShopItem(shopItemId, shopItemUpdate);
        var updatedData = updResult.Data;

        Assert.NotNull(updResult);
        Assert.NotNull(updatedData);
        Assert.Equal(updatedData.BookId, shopItemUpdate.BookId);
        Assert.Equal(updatedData.Price, shopItemUpdate.Price);
        Assert.Equal(updatedData.Stock, shopItemUpdate.Stock);
    }

    [Fact]
    public async Task UpdateNonexistentShopItem_ShouldFail()
    {
        // Arrange
        var shopItemUpdate = new ShopItemDto { BookId = 2, Price = 30, Stock = 10 };

        // Act

        var updResult = await shopItemService.UpdateShopItem(int.MaxValue, shopItemUpdate);

        Assert.False(updResult.IsSuccess);
        Assert.NotNull(updResult.CustomErrorMessage);
    }

    [Fact]
    public async Task DeleteShopItem_ShouldReturn()
    {
        // Arrange
        var shopItem = new ShopItemDto { BookId = 1, Price = 15, Stock = 100 };

        // Act
        var result = await shopItemService.AddShopItem(shopItem);
        Assert.True(result.IsSuccess);

        var shopItemData = result.Data!;
        var shopItemId = result.Data.Id;

        var deleteResult = await shopItemService.DeleteShopItem(shopItemId);
        Assert.True(deleteResult.IsSuccess);
    }

    [Fact]
    public async Task DeleteNonexistentShopItem_ShouldFail()
    {
        // Act
        var deleteResult = await shopItemService.DeleteShopItem(int.MaxValue);

        Assert.False(deleteResult.IsSuccess);
        Assert.NotNull(deleteResult.CustomErrorMessage);
    }
}
