using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Mocks;
public static class MockedDBContext
{
    public static string RandomDBName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDBContextOptions()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
            .UseInMemoryDatabase(RandomDBName)
            .Options;

        return dbContextOptions;
    }

    public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
    {
        var dbContext = new BookHubDbContext(options);
        dbContext.Database.EnsureCreated();
        PrepareData(dbContext);
        return dbContext;
    }

    private static void PrepareData(BookHubDbContext dbContext)
    {
        var user = new UserEntity
        {
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
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var author = new AuthorEntity
        {
            FirstName = "Test",
            LastName = "Author"
        };
        dbContext.Authors.Add(author);
        dbContext.SaveChanges();

        var genre = new GenreEntity
        {
            Name = "Test Genre"
        };
        dbContext.Genres.Add(genre);
        dbContext.SaveChanges();

        var publisher = new PublisherEntity
        {
            Name = "Test Publisher"
        };
        dbContext.Publishers.Add(publisher);
        dbContext.SaveChanges();

        var book = new BookEntity
        {
            Title = "Test Book",
            Author = author,
            Genre = genre,
            Publisher = publisher,
            Isbn = "1234567890",
            Description = "Test Description",
        };
        dbContext.Books.Add(book);
        dbContext.SaveChanges();

        var shopItem = new ShopItemEntity
        {
            Book = book,
            Price = 100,
            Stock = 10
        };
        dbContext.ShopItems.Add(shopItem);
        dbContext.SaveChanges();

        var order = new OrderEntity
        {
            User = user,
            Address = user.Addresses.FirstOrDefault() ?? throw new Exception("User has no address"),
            PlacedDate = DateTime.Now,
            State = OrderState.Paid,
            TotalPrice = 100,
        };
        dbContext.Orders.Add(order);
        dbContext.SaveChanges();
    }

}

