using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Services;
public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IRatingService, RatingService>();
        services.AddTransient<IWishlistService, WishlistService>();
        services.AddTransient<IShopItemService, ShopItemService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<IGiftCardService, GiftCardService>();
        services.AddTransient<ICouponCodeService, CouponCodeService>();
        services.AddTransient<IAuthorService, AuthorService>();
        services.AddTransient<IGenreService, GenreService>();
        services.AddTransient<IPublisherService, PublisherService>();

        string logFile = GetLogFilePath();
        services.AddSingleton(new LiteDatabase($"Filename={logFile}; Connection=shared"));
    }

    private static string GetLogFilePath()
    {
        var currentDir = Path.GetFullPath(Environment.CurrentDirectory.ToString());
        var parentDir = Directory.GetParent(currentDir).FullName;
        var logsDir = Path.Combine(parentDir, "logs");

        if (!Directory.Exists(logsDir))
        {
            Directory.CreateDirectory(logsDir);
        }

        var logFile = Path.Combine(logsDir, "RequestLogs.db");

        if (!File.Exists(logFile))
        {
            using (File.Create(logFile)) { }
        }

        return logFile;
    }
}
