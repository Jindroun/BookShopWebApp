using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;
public static class DataInitializerManual
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthorEntity>().HasData(PrepairAuthorModels());
        modelBuilder.Entity<GenreEntity>().HasData(PrepairGenreModels());
        modelBuilder.Entity<PublisherEntity>().HasData(PrepairPublisherModels());
        modelBuilder.Entity<BookEntity>().HasData(PrepairBookModels());
        modelBuilder.Entity<UserEntity>().HasData(PrepairUserModels());
        modelBuilder.Entity<AddressEntity>().HasData(PrepairAddressModels());
        modelBuilder.Entity<RatingEntity>().HasData(PrepairRatingModels());
        modelBuilder.Entity<WishlistEntryEntity>().HasData(PrepairWishlistEntryModels());
        modelBuilder.Entity<ShopItemEntity>().HasData(PrepairShopItemModels());
        modelBuilder.Entity<LocalIdentityUser>().HasData(PrepairIdentityUsers());

        var orderItems = PrepairOrderItemModels();
        modelBuilder.Entity<OrderEntity>().HasData(PrepairOrderModels(orderItems));
        modelBuilder.Entity<OrderItemEntity>().HasData(orderItems);
    }

    private static List<AuthorEntity> PrepairAuthorModels()
    {
        return
        [
            new AuthorEntity
            {
                Id = 1,
                FirstName = "George",
                LastName = "Orwell"
            },
            new AuthorEntity
            {
                Id = 2,
                FirstName = "J.R.R.",
                LastName = "Tolkien"
            },
            new AuthorEntity
            {
                Id = 3,
                FirstName = "J.K.",
                LastName = "Rowling"
            },
            new AuthorEntity
            {
                Id = 4,
                FirstName = "Stephen",
                LastName = "King"
            },
            new AuthorEntity
            {
                Id = 5,
                FirstName = "Agatha",
                LastName = "Christie"
            }
        ];
    }

    private static List<GenreEntity> PrepairGenreModels()
    {
        return
        [
            new GenreEntity
            {
                Id = 1,
                Name = "Science Fiction"
            },
            new GenreEntity
            {
                Id = 2,
                Name = "Fantasy"
            },
            new GenreEntity
            {
                Id = 3,
                Name = "Mystery"
            },
            new GenreEntity
            {
                Id = 4,
                Name = "Horror"
            }
        ];
    }

    private static List<PublisherEntity> PrepairPublisherModels()
    {
        return
        [
            new PublisherEntity
            {
                Id = 1,
                Name = "Penguin Random House"
            },
            new PublisherEntity
            {
                Id = 2,
                Name = "HarperCollins"
            },
            new PublisherEntity
            {
                Id = 3,
                Name = "Simon & Schuster"
            },
            new PublisherEntity
            {
                Id = 4,
                Name = "Hachette Livre"
            }
        ];
    }

    private static List<BookEntity> PrepairBookModels()
    {
        return new List<BookEntity>
    {
        new BookEntity
        {
            Id = 1,
            Title = "1984",
            AuthorId = 1,
            PublisherId = 1,
            GenreId = 1,
            Isbn = "9780451524935",
            Description = "A dystopian novel set in a totalitarian regime, where Big Brother watches over all citizens."
        },
        new BookEntity
        {
            Id = 2,
            Title = "The Lord of the Rings",
            AuthorId = 2,
            PublisherId = 2,
            GenreId = 2,
            Isbn = "9780618640157",
            Description = "An epic fantasy trilogy following the journey to destroy the One Ring and defeat the dark lord Sauron."
        },
        new BookEntity
        {
            Id = 3,
            Title = "Harry Potter and the Philosopher's Stone",
            AuthorId = 3,
            PublisherId = 3,
            GenreId = 2,
            Isbn = "9780747532743",
            Description = "The first book in the Harry Potter series, introducing the young wizard and his journey at Hogwarts."
        },
        new BookEntity
        {
            Id = 4,
            Title = "The Shining",
            AuthorId = 4,
            PublisherId = 4,
            GenreId = 4,
            Isbn = "9780307743657",
            Description = "A psychological horror novel about a family's terrifying experience in a haunted hotel."
        }
    };
    }

private static List<UserEntity> PrepairUserModels()
{
    return new List<UserEntity>
    {
        new UserEntity
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Ratings = new List<RatingEntity>(),
        },
        new UserEntity
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            Ratings = new List<RatingEntity>(),
        },
        new UserEntity
        {
            Id = 3,
            FirstName = "Alex",
            LastName = "Johnson",
            Ratings = new List<RatingEntity>(),
        },
        new UserEntity
        {
            Id = 4,
            FirstName = "Emily",
            LastName = "Davis",
            Ratings = new List<RatingEntity>(),
        },
        new UserEntity
        {
            Id = 5,
            FirstName = "Michael",
            LastName = "Brown",
            Ratings = new List<RatingEntity>(),
        }
    };
}
    private static List<LocalIdentityUser> PrepairIdentityUsers()
    {
        return new List<LocalIdentityUser>
    {
        new LocalIdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "john.doe@example.com",
            Email = "john.doe@example.com",
            UserId = 1 // Links to UserEntity with Id 1
        },
        new LocalIdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "jane.smith@example.com",
            Email = "jane.smith@example.com",
            UserId = 2 // Links to UserEntity with Id 2
        },
        new LocalIdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "alex.johnson@example.com",
            Email = "alex.johnson@example.com",
            UserId = 3 // Links to UserEntity with Id 3
        },
        new LocalIdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "emily.davis@example.com",
            Email = "emily.davis@example.com",
            UserId = 4 // Links to UserEntity with Id 4
        },
        new LocalIdentityUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "michael.brown@example.com",
            Email = "michael.brown@example.com",
            UserId = 5 // Links to UserEntity with Id 5
        }
    };
    }


    private static List<AddressEntity> PrepairAddressModels()
    {
        return new List<AddressEntity>
    {
        new AddressEntity
        {
            Id = 1,
            UserId = 1, // Links to User with Id 1
            Street = "456 Oak Street",
            City = "Springfield",
            PostalCode = "12345",
            Country = "USA"
        },
        new AddressEntity
        {
            Id = 2,
            UserId = 2, // Links to User with Id 2
            Street = "789 Maple Avenue",
            City = "Rivertown",
            PostalCode = "23456",
            Country = "USA"
        },
        new AddressEntity
        {
            Id = 3,
            UserId = 3, // Links to User with Id 3
            Street = "101 Pine Road",
            City = "Lakeside",
            PostalCode = "34567",
            Country = "USA"
        },
        new AddressEntity
        {
            Id = 4,
            UserId = 4, // Links to User with Id 4
            Street = "202 Cedar Boulevard",
            City = "Greenfield",
            PostalCode = "98765",
            Country = "USA"
        },
        new AddressEntity
        {
            Id = 5,
            UserId = 5, // Links to User with Id 5
            Street = "303 Birch Lane",
            City = "Willowtown",
            PostalCode = "56789",
            Country = "USA"
        }
    };
    }


    private static List<RatingEntity> PrepairRatingModels()
    {
        return new List<RatingEntity>
    {
        new RatingEntity
        {
            Id = 1,
            UserId = 1,  // John Doe
            BookId = 1,  // 1984
            StarRating = 5,
            Note = "A chilling depiction of a dystopian future."
        },
        new RatingEntity
        {
            Id = 2,
            UserId = 2,  // Jane Smith
            BookId = 2,  // The Lord of the Rings
            StarRating = 5,
            Note = "An epic journey filled with lore and adventure."
        },
        new RatingEntity
        {
            Id = 3,
            UserId = 3,  // Alex Johnson
            BookId = 3,  // Harry Potter and the Philosopher's Stone
            StarRating = 5,
            Note = "A magical beginning to an iconic series."
        },
        new RatingEntity
        {
            Id = 4,
            UserId = 4,  // Emily Davis
            BookId = 4,  // The Shining
            StarRating = 4,
            Note = "A terrifying descent into madness."
        },
        new RatingEntity
        {
            Id = 5,
            UserId = 5,  // Michael Brown
            BookId = 1,  // 1984 again for variety
            StarRating = 5,
            Note = "Thought-provoking and eerily relevant."
        }
    };
    }

    private static List<WishlistEntryEntity> PrepairWishlistEntryModels()
    {
        return new List<WishlistEntryEntity>
        {
            new WishlistEntryEntity { Id = 1, UserId = 1, BookId = 1 },
            new WishlistEntryEntity { Id = 2, UserId = 1, BookId = 2 },
            new WishlistEntryEntity { Id = 3, UserId = 2, BookId = 1 },
            new WishlistEntryEntity { Id = 4, UserId = 3, BookId = 3 }

        };
    }

    private static List<ShopItemEntity> PrepairShopItemModels()
    {
        return new List<ShopItemEntity>
        {
            new ShopItemEntity
            {
                Id = 1,
                BookId = 1,
                Price = 10m,
                Stock = 100
            },
            new ShopItemEntity
            {
                Id = 2,
                BookId = 2,
                Price = 20m,
                Stock = 50
            },
            new ShopItemEntity
            {
                Id = 3,
                BookId = 3,
                Price = 15m,
                Stock = 75
            },
            new ShopItemEntity
            {
                Id = 4,
                BookId = 4,
                Price = 12m,
                Stock = 0
            }
        };
    }

    private static List<OrderEntity> PrepairOrderModels(List<OrderItemEntity> orderItems)
    {
        var orders = new List<OrderEntity>
        {
            new OrderEntity
            {
                Id = 1,
                UserId = 1,
                AddressId = 1,
                PlacedDate = new DateTime(2000, 1, 1),
                State = OrderState.Paid,
            },
            new OrderEntity
            {
                Id = 2,
                UserId = 2,
                AddressId = 2,
                PlacedDate = new DateTime(2000, 1, 2),
                State = OrderState.Fullfilled,
            },
            new OrderEntity
            {
                Id = 3,
                UserId = 2,
                AddressId = 2,
                PlacedDate = new DateTime(2000, 1, 1),
                State = OrderState.Cancelled,
            },
        };
        foreach (var order in orders)
        {
            order.TotalPrice = orderItems.Where(oi => oi.OrderId == order.Id).Sum(oi => oi.PricePerItem * oi.Count);
        }
        return orders;
    }


    private static List<OrderItemEntity> PrepairOrderItemModels()
    {
        return new List<OrderItemEntity>
        {
            new OrderItemEntity
            {
                OrderId = 1,
                ShopItemId = 1,
                Count = 2,
                PricePerItem = 10m
            },
            new OrderItemEntity
            {
                OrderId = 1,
                ShopItemId = 2,
                Count = 1,
                PricePerItem = 20m
            },
            new OrderItemEntity
            {
                OrderId = 2,
                ShopItemId = 3,
                Count = 3,
                PricePerItem = 16m
            }
        };
    }
}


