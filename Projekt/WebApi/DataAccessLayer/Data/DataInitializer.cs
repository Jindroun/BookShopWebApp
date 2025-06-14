using Bogus;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var authors = PrepareAuthorModels();
            modelBuilder.Entity<AuthorEntity>().HasData(authors);

            var genres = PrepareGenreModels();
            modelBuilder.Entity<GenreEntity>().HasData(genres);

            var publishers = PreparePublisherModels();
            modelBuilder.Entity<PublisherEntity>().HasData(publishers);

            var users = PrepareUserModels();
            modelBuilder.Entity<UserEntity>().HasData(users);

            var addresses = PrepareAddressModels();
            modelBuilder.Entity<AddressEntity>().HasData(addresses);

            var books = PrepareBookModels(authors, publishers, genres);
            modelBuilder.Entity<BookEntity>().HasData(books.Select(b => new
            {
                b.Id,
                b.Title,
                b.AuthorId,
                b.PublisherId,
                b.GenreId,
                b.Isbn,
                b.Description,
                b.CreatedAt,
                b.UpdatedAt
            }));

            var shopItems = PrepareShopItemModels(books);
            modelBuilder.Entity<ShopItemEntity>().HasData(shopItems.Select(s => new
            {
                s.Id,
                s.BookId,
                s.Price,
                s.Stock,
                s.CreatedAt,
                s.UpdatedAt
            }));

            var ratings = PrepareRatingModels(users, books);
            modelBuilder.Entity<RatingEntity>().HasData(ratings.Select(r => new
            {
                r.Id,
                r.UserId,
                r.BookId,
                r.StarRating,
                r.Note,
                r.CreatedAt,
                r.UpdatedAt
            }));

            var wishlistEntries = PrepareWishlistEntryModels(users, books);
            modelBuilder.Entity<WishlistEntryEntity>().HasData(wishlistEntries.Select(w => new
            {
                w.Id,
                w.UserId,
                w.BookId,
                w.CreatedAt,
                w.UpdatedAt
            }));

            var orders = PrepareOrderModels(users, addresses);
            modelBuilder.Entity<OrderEntity>().HasData(orders.Select(o => new
            {
                o.Id,
                o.UserId,
                o.AddressId,
                o.PlacedDate,
                o.State,
                o.TotalPrice,
                o.CreatedAt,
                o.UpdatedAt
            }));

            var orderItems = PrepareOrderItemModels(orders, shopItems);
            modelBuilder.Entity<OrderItemEntity>().HasData(orderItems.Select(oi => new
            {
                oi.OrderId,
                oi.ShopItemId,
                oi.Count,
                oi.PricePerItem
            }));

            var identityUsers = PrepareIdentityUsers(users);
            modelBuilder.Entity<LocalIdentityUser>().HasData(identityUsers.Select(ui => new
            {
                ui.Id,
                ui.UserName,
                ui.NormalizedUserName,
                ui.Email,
                ui.NormalizedEmail,
                ui.EmailConfirmed,
                ui.PasswordHash,
                ui.SecurityStamp,
                ui.ConcurrencyStamp,
                ui.PhoneNumber,
                ui.PhoneNumberConfirmed,
                ui.TwoFactorEnabled,
                ui.LockoutEnd,
                ui.LockoutEnabled,
                ui.AccessFailedCount,
                ui.UserId
            }));

            var giftCards = PrepareGiftCardModels();
            modelBuilder.Entity<GiftCardEntity>().HasData(giftCards);
            var couponCodes = PrepareCouponCodeModels(giftCards, orders);
            modelBuilder.Entity<CouponCodeEntity>().HasData(couponCodes);

            var bookGenres = PrepareBookGenres(books, genres);
            modelBuilder.Entity<BookGenreEntity>().HasData(bookGenres);
        }

        private static List<BookGenreEntity> PrepareBookGenres(List<BookEntity> books, List<GenreEntity> genres, int count = 10)
        {
            var bookGenres = new List<BookGenreEntity>();
            var faker = new Faker();
            // Assign secondary genres
            for (int j = 0; j < count; j++) 
            {
                var genre = faker.PickRandom(genres);
                var book = faker.PickRandom(books);
                if (book.GenreId != genre.Id && !bookGenres.Any(bg => bg.SecondaryGenresId == genre.Id && bg.SecondaryBooksId == book.Id))
                {
                    bookGenres.Add(new BookGenreEntity
                    {
                        SecondaryBooksId = book.Id,
                        SecondaryGenresId = genre.Id
                    });
                }
            }
            return bookGenres;
        }


        private static List<GiftCardEntity> PrepareGiftCardModels(int count = 5)
        {
            var giftCards = new List<GiftCardEntity>();
            var faker = new Faker();

            for (int i = 1; i <= count; i++)
            {
                giftCards.Add(new GiftCardEntity
                {
                    Id = i,
                    Discount = faker.Random.Decimal(1m, 500m),
                    ValidFrom = DateTime.UtcNow.AddYears(-2),
                    ValidTo = DateTime.UtcNow.AddDays(faker.Random.Int(-200, 1000)),
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                });
            }

            return giftCards;
        }

        private static List<CouponCodeEntity> PrepareCouponCodeModels(List<GiftCardEntity> giftCards, List<OrderEntity> orders, int count = 10)
        {
            var couponCodes = new List<CouponCodeEntity>();
            var faker = new Faker();

            for (int i = 1; i <= count; i++)
            {
                var giftCard = faker.PickRandom(giftCards);
                var cc = new CouponCodeEntity
                {
                    Id = i,
                    GiftCardId = giftCard.Id,
                    //GiftCard = giftCard,
                    Code = faker.Random.AlphaNumeric(6),
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                };
                couponCodes.Add(cc);

                // assign to orders if dates fit
                var order = orders[faker.Random.Int(0, orders.Count - 1)];
                bool validUsage = order.CouponCode == null && order.PlacedDate >= giftCard.ValidFrom && order.PlacedDate <= giftCard.ValidTo;
                if (validUsage)
                {
                    //order.CouponCode = cc;
                    order.CouponCodeId = cc.Id;
                    //cc.Order = order;
                }
            }

            return couponCodes;
        }


        private static List<AuthorEntity> PrepareAuthorModels()
        {
            return new List<AuthorEntity>
            {
                new AuthorEntity { Id = 1, FirstName = "George", LastName = "Orwell", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AuthorEntity { Id = 2, FirstName = "J.K.", LastName = "Rowling", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AuthorEntity { Id = 3, FirstName = "Agatha", LastName = "Christie", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AuthorEntity { Id = 4, FirstName = "Stephen", LastName = "King", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AuthorEntity { Id = 5, FirstName = "Jane", LastName = "Austen", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) }
            };
        }

        private static List<GenreEntity> PrepareGenreModels()
        {
            return new List<GenreEntity>
            {
                new GenreEntity { Id = 1, Name = "Science Fiction", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GenreEntity { Id = 2, Name = "Fantasy", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GenreEntity { Id = 3, Name = "Mystery", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GenreEntity { Id = 4, Name = "Horror", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GenreEntity { Id = 5, Name = "Romance", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new GenreEntity { Id = 6, Name = "Non-Fiction", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
        }

        private static List<PublisherEntity> PreparePublisherModels()
        {
            return new List<PublisherEntity>
            {
                new PublisherEntity { Id = 1, Name = "Penguin Random House", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new PublisherEntity { Id = 2, Name = "HarperCollins", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new PublisherEntity { Id = 3, Name = "Simon & Schuster", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new PublisherEntity { Id = 4, Name = "Hachette Livre", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new PublisherEntity { Id = 5, Name = "Macmillan Publishers", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
        }

        private static List<UserEntity> PrepareUserModels()
        {
            return new List<UserEntity>
            {
                new UserEntity { Id = 1, FirstName = "Alice", LastName = "Smith", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 2, FirstName = "Bob", LastName = "Johnson", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 3, FirstName = "Charlie", LastName = "Williams", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 4, FirstName = "Diana", LastName = "Brown", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 5, FirstName = "Ethan", LastName = "Jones", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 6, FirstName = "Fiona", LastName = "Garcia", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 7, FirstName = "George", LastName = "Miller", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 8, FirstName = "Hannah", LastName = "Davis", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 9, FirstName = "Ian", LastName = "Martinez", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new UserEntity { Id = 10, FirstName = "Julia", LastName = "Hernandez", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) }
            };
        }

        private static List<AddressEntity> PrepareAddressModels()
        {
            var addresses = new List<AddressEntity>
            {
                new AddressEntity { Id = 1, UserId = 1, Street = "123 Maple Street", City = "Springfield", PostalCode = "12345", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 2, UserId = 2, Street = "456 Oak Avenue", City = "Shelbyville", PostalCode = "23456", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 3, UserId = 3, Street = "789 Pine Road", City = "Ogdenville", PostalCode = "34567", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 4, UserId = 4, Street = "321 Birch Boulevard", City = "North Haverbrook", PostalCode = "45678", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 5, UserId = 5, Street = "654 Cedar Lane", City = "Capital City", PostalCode = "56789", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 6, UserId = 6, Street = "987 Walnut Street", City = "Springfield", PostalCode = "67890", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 7, UserId = 7, Street = "147 Elm Avenue", City = "Shelbyville", PostalCode = "78901", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 8, UserId = 8, Street = "258 Poplar Road", City = "Ogdenville", PostalCode = "89012", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 9, UserId = 9, Street = "369 Ash Boulevard", City = "North Haverbrook", PostalCode = "90123", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) },
                new AddressEntity { Id = 10, UserId = 10, Street = "159 Spruce Lane", City = "Capital City", PostalCode = "01234", Country = "USA", CreatedAt = DateTime.UtcNow.AddYears(-2), UpdatedAt = DateTime.UtcNow.AddYears(-1) }
            };

            return addresses;
        }

        private static List<BookEntity> PrepareBookModels(List<AuthorEntity> authors, List<PublisherEntity> publishers, List<GenreEntity> genres)
        {
            var faker = new Faker();
            var books = new List<BookEntity>
            {
                new BookEntity
                {
                    Id = 1,
                    Title = "1984",
                    AuthorId = 1,
                    PublisherId = 1,
                    GenreId = 1,
                    Isbn = "9780451524935",
                    Description = "A dystopian novel set in a totalitarian society.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
                new BookEntity
                {
                    Id = 2,
                    Title = "Animal Farm",
                    AuthorId = 1,
                    PublisherId = 1,
                    GenreId = 1,
                    Isbn = "9780451526342",
                    Description = "An allegorical novella reflecting events leading up to the Russian Revolution.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
                new BookEntity
                {
                    Id = 3,
                    Title = "Harry Potter and the Sorcerer's Stone",
                    AuthorId = 2,
                    PublisherId = 2,
                    GenreId = 2,
                    Isbn = "9780439708180",
                    Description = "The first book in the Harry Potter series.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
                new BookEntity
                {
                    Id = 4,
                    Title = "Harry Potter and the Chamber of Secrets",
                    AuthorId = 2,
                    PublisherId = 2,
                    GenreId = 2,
                    Isbn = "9780439064873",
                    Description = "The second book in the Harry Potter series.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
                new BookEntity
                {
                    Id = 5,
                    Title = "Murder on the Orient Express",
                    AuthorId = 3,
                    PublisherId = 3,
                    GenreId = 3,
                    Isbn = "9780062693662",
                    Description = "A detective novel featuring Hercule Poirot.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
                new BookEntity
                {
                    Id = 6,
                    Title = "The Shining",
                    AuthorId = 4,
                    PublisherId = 4,
                    GenreId = 4,
                    Isbn = "9780307743657",
                    Description = "A horror novel about a haunted hotel.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
                new BookEntity
                {
                    Id = 7,
                    Title = "Pride and Prejudice",
                    AuthorId = 5,
                    PublisherId = 5,
                    GenreId = 5,
                    Isbn = "9780141439518",
                    Description = "A classic romance novel.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                },
            };

            for (int i = 8; i <= 20; i++)
            {
                books.Add(new BookEntity
                {
                    Id = i,
                    Title = $"Sample Book {i}",
                    AuthorId = (i % authors.Count) + 1,
                    PublisherId = (i % publishers.Count) + 1,
                    GenreId = (i % genres.Count) + 1,
                    Isbn = $"978000000000{i}",
                    Description = $"Description for Sample Book {i}.",
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                });
            }

            return books;
        }

        private static List<ShopItemEntity> PrepareShopItemModels(List<BookEntity> books)
        {
            var shopItems = new List<ShopItemEntity>();
            int id = 1;
            foreach (var book in books)
            {
                shopItems.Add(new ShopItemEntity
                {
                    Id = id,
                    BookId = book.Id,
                    Price = 19.99m + id,
                    Stock = 100 + id,
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                });
                id++;
            }
            return shopItems;
        }

        private static List<RatingEntity> PrepareRatingModels(List<UserEntity> users, List<BookEntity> books)
        {
            var ratings = new List<RatingEntity>();
            int id = 1;
            var faker = new Faker();

            foreach (var user in users)
            {
                var ratedBooks = books.OrderBy(b => b.Id).Skip((user.Id - 1) % books.Count).Take(3).ToList();
                foreach (var book in ratedBooks)
                {
                    ratings.Add(new RatingEntity
                    {
                        Id = id++,
                        UserId = user.Id,
                        BookId = book.Id,
                        StarRating = faker.Random.Int(1, 5),
                        Note = faker.Lorem.Sentences(2),
                        CreatedAt = DateTime.UtcNow.AddYears(-2),
                        UpdatedAt = DateTime.UtcNow.AddYears(-1)
                    });
                }
            }

            while (ratings.Count < 30)
            {
                var user = users[faker.Random.Int(0, users.Count - 1)];
                var book = books[faker.Random.Int(0, books.Count - 1)];
                if (!ratings.Any(r => r.UserId == user.Id && r.BookId == book.Id))
                {
                    ratings.Add(new RatingEntity
                    {
                        Id = id++,
                        UserId = user.Id,
                        BookId = book.Id,
                        StarRating = faker.Random.Int(1, 5),
                        Note = faker.Lorem.Sentences(2),
                        CreatedAt = DateTime.UtcNow.AddYears(-2),
                        UpdatedAt = DateTime.UtcNow.AddYears(-1)
                    });
                }
            }

            return ratings;
        }

        private static List<WishlistEntryEntity> PrepareWishlistEntryModels(List<UserEntity> users, List<BookEntity> books)
        {
            var wishlistEntries = new List<WishlistEntryEntity>();
            int id = 1;
            var faker = new Faker();

            foreach (var user in users)
            {
                var wishedBooks = books.OrderBy(b => b.Id).Skip((user.Id - 1) % books.Count).Take(2).ToList();
                foreach (var book in wishedBooks)
                {
                    wishlistEntries.Add(new WishlistEntryEntity
                    {
                        Id = id++,
                        UserId = user.Id,
                        BookId = book.Id,
                        CreatedAt = DateTime.UtcNow.AddYears(-2),
                        UpdatedAt = DateTime.UtcNow.AddYears(-1)
                    });
                }
            }

            while (wishlistEntries.Count < 25)
            {
                var user = users[faker.Random.Int(0, users.Count - 1)];
                var book = books[faker.Random.Int(0, books.Count - 1)];
                if (!wishlistEntries.Any(w => w.UserId == user.Id && w.BookId == book.Id))
                {
                    wishlistEntries.Add(new WishlistEntryEntity
                    {
                        Id = id++,
                        UserId = user.Id,
                        BookId = book.Id,
                        CreatedAt = DateTime.UtcNow.AddYears(-2),
                        UpdatedAt = DateTime.UtcNow.AddYears(-1)
                    });
                }
            }

            return wishlistEntries;
        }

        private static List<OrderEntity> PrepareOrderModels(List<UserEntity> users, List<AddressEntity> addresses)
        {
            var orders = new List<OrderEntity>();
            int id = 1;
            var faker = new Faker();

            for (int i = 0; i < 15; i++)
            {
                if (i == 0)
                {
                    orders.Add(new OrderEntity
                    {
                        Id = id,
                        UserId = 1,
                        AddressId = addresses.FirstOrDefault(a => a.UserId == 1)?.Id ?? 1,
                        PlacedDate = DateTime.UtcNow.AddMonths(-1),
                        State = OrderState.Paid,
                        TotalPrice = 0m,
                        CreatedAt = DateTime.UtcNow.AddYears(-2),
                        UpdatedAt = DateTime.UtcNow.AddYears(-1)
                    });

                    id++;
                    continue;
                }

                var user = users[faker.Random.Int(0, users.Count - 1)];
                var address = addresses.FirstOrDefault(a => a.UserId == user.Id) ?? addresses[0];

                orders.Add(new OrderEntity
                {
                    Id = id,
                    UserId = user.Id,
                    AddressId = address.Id,
                    PlacedDate = DateTime.UtcNow.AddMonths(-faker.Random.Int(1, 24)),
                    State = faker.PickRandom<OrderState>(),
                    TotalPrice = 0m,
                    CreatedAt = DateTime.UtcNow.AddYears(-2),
                    UpdatedAt = DateTime.UtcNow.AddYears(-1)
                });
                id++;
            }

            return orders;
        }

        private static List<OrderItemEntity> PrepareOrderItemModels(List<OrderEntity> orders, List<ShopItemEntity> shopItems)
        {
            var orderItems = new List<OrderItemEntity>();
            var faker = new Faker();

            foreach (var order in orders)
            {
                int itemsCount = faker.Random.Int(1, Math.Min(5, shopItems.Count));
                var selectedShopItems = shopItems.OrderBy(s => s.Id).Take(itemsCount).ToList();

                foreach (var shopItem in selectedShopItems)
                {
                    var orderItem = new OrderItemEntity
                    {
                        OrderId = order.Id,
                        ShopItemId = shopItem.Id,
                        Count = faker.Random.Int(1, 5),
                        PricePerItem = shopItem.Price
                    };
                    orderItems.Add(orderItem);

                    order.TotalPrice += orderItem.PricePerItem * orderItem.Count;
                }
            }

            return orderItems;
        }

        private static List<LocalIdentityUser> PrepareIdentityUsers(List<UserEntity> users)
        {
            var identityUsers = new List<LocalIdentityUser>();
            var faker = new Faker();

            foreach (var user in users)
            {
                identityUsers.Add(new LocalIdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = $"{user.FirstName.ToLower()}{user.Id}@example.com",
                    NormalizedUserName = $"{user.FirstName.ToUpper()}{user.Id}@EXAMPLE.COM",
                    Email = $"{user.FirstName.ToLower()}{user.Id}@example.com",
                    NormalizedEmail = $"{user.FirstName.ToUpper()}{user.Id}@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = faker.Phone.PhoneNumber(),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0,
                    UserId = user.Id
                });
            }

            return identityUsers;
        }
    }
}
