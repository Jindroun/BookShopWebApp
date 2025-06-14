using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTests.Mocks;
public class MockedDependencyInjectionBuilder
{
    protected IServiceCollection _serviceCollection = new ServiceCollection();

    public MockedDependencyInjectionBuilder()
    {
    }

    public MockedDependencyInjectionBuilder AddMockdDBContext()
    {
        var dbOptions = MockedDBContext.GenerateNewInMemoryDBContextOptions();
        var dbContext = MockedDBContext.CreateFromOptions(dbOptions);
        _serviceCollection = _serviceCollection
            .AddScoped<BookHubDbContext>(_ => dbContext);

        return this;
    }

    public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
        where T : class
    {
        _serviceCollection = _serviceCollection
            .AddScoped<T>(_ => objectToRegister);

        return this;
    }

    public ServiceProvider Create()
    {
        return _serviceCollection.BuildServiceProvider();
    }

    public MockedDependencyInjectionBuilder AddServices()
    {
        _serviceCollection = _serviceCollection
            .AddScoped<UserManager<LocalIdentityUser>>();

        _serviceCollection = _serviceCollection
            .AddScoped<IUserService, UserService>()
            .AddScoped<IOrderService, OrderService>()
            .AddScoped<IBookService, BookService>()
            .AddScoped<IRatingService, RatingService>()
            .AddScoped<IShopItemService, ShopItemService>();

        return this;
    }

    public MockedDependencyInjectionBuilder AddMapper()
    {
        _serviceCollection = _serviceCollection
            .AddAutoMapper(typeof(BusinessLayer.AutoMapperProfile));

        return this;
    }
}
