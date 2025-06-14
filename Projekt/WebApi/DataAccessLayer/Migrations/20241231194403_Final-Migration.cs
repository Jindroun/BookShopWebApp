using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    EntityName = table.Column<string>(type: "TEXT", nullable: false),
                    EntityId = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Changes = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Discount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouponCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    GiftCardId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponCodes_GiftCards_GiftCardId",
                        column: x => x.GiftCardId,
                        principalTable: "GiftCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorId = table.Column<int>(type: "INTEGER", nullable: false),
                    PublisherId = table.Column<int>(type: "INTEGER", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    SecondaryBooksId = table.Column<int>(type: "INTEGER", nullable: false),
                    SecondaryGenresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.SecondaryBooksId, x.SecondaryGenresId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_SecondaryBooksId",
                        column: x => x.SecondaryBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_SecondaryGenresId",
                        column: x => x.SecondaryGenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    StarRating = table.Column<int>(type: "INTEGER", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wishlists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlacedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    CouponCodeId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_CouponCodes_CouponCodeId",
                        column: x => x.CouponCodeId,
                        principalTable: "CouponCodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    ShopItemId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ShopItemId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_ShopItems_ShopItemId",
                        column: x => x.ShopItemId,
                        principalTable: "ShopItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7399), "George", "Orwell", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7404) },
                    { 2, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7408), "J.K.", "Rowling", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7408) },
                    { 3, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7411), "Agatha", "Christie", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7412) },
                    { 4, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7415), "Stephen", "King", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7415) },
                    { 5, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7418), "Jane", "Austen", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7418) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7496), "Science Fiction", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7496) },
                    { 2, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7500), "Fantasy", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7501) },
                    { 3, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7503), "Mystery", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7504) },
                    { 4, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7507), "Horror", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7507) },
                    { 5, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7510), "Romance", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7510) },
                    { 6, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7537), "Non-Fiction", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7537) }
                });

            migrationBuilder.InsertData(
                table: "GiftCards",
                columns: new[] { "Id", "CreatedAt", "Discount", "UpdatedAt", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8138), 411.611015554923367m, new DateTime(2023, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8139), new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8133), new DateTime(2024, 12, 9, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8135) },
                    { 2, new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8153), 286.73494870765435m, new DateTime(2023, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8153), new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8152), new DateTime(2026, 11, 17, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8152) },
                    { 3, new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8157), 437.629309947708743m, new DateTime(2023, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8157), new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8156), new DateTime(2025, 5, 15, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8157) },
                    { 4, new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8161), 175.423499310643607m, new DateTime(2023, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8162), new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8161), new DateTime(2024, 9, 17, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8161) },
                    { 5, new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8165), 273.307363420679901m, new DateTime(2023, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8166), new DateTime(2022, 12, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8165), new DateTime(2027, 1, 31, 19, 44, 2, 698, DateTimeKind.Utc).AddTicks(8165) }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7562), "Penguin Random House", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7563) },
                    { 2, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7566), "HarperCollins", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7567) },
                    { 3, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7569), "Simon & Schuster", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7569) },
                    { 4, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7572), "Hachette Livre", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7572) },
                    { 5, new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7575), "Macmillan Publishers", new DateTime(2024, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7575) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7599), "Alice", "Smith", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7599) },
                    { 2, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7604), "Bob", "Johnson", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7604) },
                    { 3, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7607), "Charlie", "Williams", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7608) },
                    { 4, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7610), "Diana", "Brown", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7611) },
                    { 5, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7614), "Ethan", "Jones", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7614) },
                    { 6, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7618), "Fiona", "Garcia", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7618) },
                    { 7, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7621), "George", "Miller", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7622) },
                    { 8, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7692), "Hannah", "Davis", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7693) },
                    { 9, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7696), "Ian", "Martinez", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7696) },
                    { 10, new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7699), "Julia", "Hernandez", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7700) }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "PostalCode", "Street", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Springfield", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7729), "12345", "123 Maple Street", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7729), 1 },
                    { 2, "Shelbyville", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7734), "23456", "456 Oak Avenue", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7735), 2 },
                    { 3, "Ogdenville", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7738), "34567", "789 Pine Road", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7739), 3 },
                    { 4, "North Haverbrook", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7742), "45678", "321 Birch Boulevard", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7742), 4 },
                    { 5, "Capital City", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7745), "56789", "654 Cedar Lane", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7746), 5 },
                    { 6, "Springfield", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7749), "67890", "987 Walnut Street", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7749), 6 },
                    { 7, "Shelbyville", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7753), "78901", "147 Elm Avenue", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7753), 7 },
                    { 8, "Ogdenville", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7756), "89012", "258 Poplar Road", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7757), 8 },
                    { 9, "North Haverbrook", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7760), "90123", "369 Ash Boulevard", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7760), 9 },
                    { 10, "Capital City", "USA", new DateTime(2022, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7763), "01234", "159 Spruce Lane", new DateTime(2023, 12, 31, 19, 44, 2, 690, DateTimeKind.Utc).AddTicks(7764), 10 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[,]
                {
                    { "02eef335-c8e8-4059-a7db-edd48bb2c20d", 0, "cacfc3a6-c155-4e16-8990-3cf7143a12a9", "charlie3@example.com", true, true, null, "CHARLIE3@EXAMPLE.COM", "CHARLIE3@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "994-306-6529", false, "a8ce80cd-b551-4cda-9069-4ddd98c273e5", false, 3, "charlie3@example.com" },
                    { "0a6f9da9-8b31-4937-a03d-0045a1a69db7", 0, "790c5854-f673-4b19-ae0b-206e9ca8b9f3", "hannah8@example.com", true, true, null, "HANNAH8@EXAMPLE.COM", "HANNAH8@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "1-527-753-7331", false, "1e9153e0-98b3-48ba-9ad4-e4f8448a426f", false, 8, "hannah8@example.com" },
                    { "18753094-b6aa-40a2-84b8-02463e5209b4", 0, "9fa30bae-e88d-4533-9cdb-d4f053a0f135", "julia10@example.com", true, true, null, "JULIA10@EXAMPLE.COM", "JULIA10@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "1-780-469-0640 x9764", false, "457ba98d-8a5f-41e3-b799-4b90d0071b5f", false, 10, "julia10@example.com" },
                    { "3563f841-af9b-4e50-a285-3d84680d5089", 0, "b85becfa-a715-4bb8-8fce-c1febe87920f", "bob2@example.com", true, true, null, "BOB2@EXAMPLE.COM", "BOB2@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "(415) 730-8184 x8606", false, "2f7f9cc4-fb96-4606-8dad-5a332764703b", false, 2, "bob2@example.com" },
                    { "38ddd96c-c339-4244-9b24-e682cfab7040", 0, "60f97780-8fcb-4074-a7c5-dda6455b7964", "fiona6@example.com", true, true, null, "FIONA6@EXAMPLE.COM", "FIONA6@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "1-760-642-2280 x284", false, "e17a6fcc-4b31-4762-9788-1ae08fe215d5", false, 6, "fiona6@example.com" },
                    { "4d4e4d59-a2ff-4be6-9077-03aba764bf89", 0, "5da0368e-01dc-4689-bdc5-685f209bc471", "ethan5@example.com", true, true, null, "ETHAN5@EXAMPLE.COM", "ETHAN5@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "1-605-982-6644 x880", false, "a3f211d8-6f0a-463e-9ed9-5835da525f40", false, 5, "ethan5@example.com" },
                    { "5f255236-58a1-408d-902d-06c2c4a1eca1", 0, "674addb3-8a61-40f5-b496-2da7560bf039", "ian9@example.com", true, true, null, "IAN9@EXAMPLE.COM", "IAN9@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "481.690.4816 x288", false, "e83e0c59-2587-4a8e-bca3-1f83ba9c33ca", false, 9, "ian9@example.com" },
                    { "6a966b4a-9358-4a51-9dcc-47bbcfc2119e", 0, "2821ff85-a55e-4dc7-8bc1-272f8e14cc6d", "diana4@example.com", true, true, null, "DIANA4@EXAMPLE.COM", "DIANA4@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "1-989-244-6794 x6915", false, "56ef8a8e-dacb-4053-a48c-1755d74cc4e3", false, 4, "diana4@example.com" },
                    { "a64dcb54-e9d2-43bb-bd86-6c1911daf672", 0, "b8f81230-4386-4846-855d-f5d36b5d515a", "alice1@example.com", true, true, null, "ALICE1@EXAMPLE.COM", "ALICE1@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "(411) 374-8704", false, "f53929f4-78a5-41d3-ae35-8cfc4d49286f", false, 1, "alice1@example.com" },
                    { "ac5c7bbd-e7e2-4088-84a8-0b9f0ffd34eb", 0, "1b6a63b2-5a05-4f0b-9e7c-d015711ed993", "george7@example.com", true, true, null, "GEORGE7@EXAMPLE.COM", "GEORGE7@EXAMPLE.COM", "AQAAAAEAACcQAAAAEJ3iUOGi/FOJZkTqXbWrX5IDHphusbdW5W+V6Z9FfrGXX4jwVYa3asxD2PZDBFVq8A==", "851-405-2136", false, "7fc30eca-5c2d-400b-b3ee-fe25d6d858f4", false, 7, "george7@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Description", "GenreId", "Isbn", "PublisherId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4916), "A dystopian novel set in a totalitarian society.", 1, "9780451524935", 1, "1984", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4921) },
                    { 2, 1, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4927), "An allegorical novella reflecting events leading up to the Russian Revolution.", 1, "9780451526342", 1, "Animal Farm", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4927) },
                    { 3, 2, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4931), "The first book in the Harry Potter series.", 2, "9780439708180", 2, "Harry Potter and the Sorcerer's Stone", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4931) },
                    { 4, 2, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4935), "The second book in the Harry Potter series.", 2, "9780439064873", 2, "Harry Potter and the Chamber of Secrets", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4935) },
                    { 5, 3, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4938), "A detective novel featuring Hercule Poirot.", 3, "9780062693662", 3, "Murder on the Orient Express", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4939) },
                    { 6, 4, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4943), "A horror novel about a haunted hotel.", 4, "9780307743657", 4, "The Shining", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4943) },
                    { 7, 5, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4947), "A classic romance novel.", 5, "9780141439518", 5, "Pride and Prejudice", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4947) },
                    { 8, 4, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4957), "Description for Sample Book 8.", 3, "9780000000008", 4, "Sample Book 8", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4957) },
                    { 9, 5, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4962), "Description for Sample Book 9.", 4, "9780000000009", 5, "Sample Book 9", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4962) },
                    { 10, 1, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4967), "Description for Sample Book 10.", 5, "97800000000010", 1, "Sample Book 10", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4967) },
                    { 11, 2, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4971), "Description for Sample Book 11.", 6, "97800000000011", 2, "Sample Book 11", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4971) },
                    { 12, 3, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4974), "Description for Sample Book 12.", 1, "97800000000012", 3, "Sample Book 12", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4975) },
                    { 13, 4, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4978), "Description for Sample Book 13.", 2, "97800000000013", 4, "Sample Book 13", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4978) },
                    { 14, 5, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4982), "Description for Sample Book 14.", 3, "97800000000014", 5, "Sample Book 14", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4982) },
                    { 15, 1, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4985), "Description for Sample Book 15.", 4, "97800000000015", 1, "Sample Book 15", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4986) },
                    { 16, 2, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4989), "Description for Sample Book 16.", 5, "97800000000016", 2, "Sample Book 16", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4989) },
                    { 17, 3, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4993), "Description for Sample Book 17.", 6, "97800000000017", 3, "Sample Book 17", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4993) },
                    { 18, 4, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4997), "Description for Sample Book 18.", 1, "97800000000018", 4, "Sample Book 18", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(4998) },
                    { 19, 5, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5001), "Description for Sample Book 19.", 2, "97800000000019", 5, "Sample Book 19", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5001) },
                    { 20, 1, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5005), "Description for Sample Book 20.", 3, "97800000000020", 1, "Sample Book 20", new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5005) }
                });

            migrationBuilder.InsertData(
                table: "CouponCodes",
                columns: new[] { "Id", "Code", "CreatedAt", "GiftCardId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "l0wzxv", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7539), 1, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7540) },
                    { 2, "j544yn", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7562), 4, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7563) },
                    { 3, "0im2p4", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7570), 5, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7570) },
                    { 4, "80cg52", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7578), 4, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7578) },
                    { 5, "ps40x5", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7585), 2, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7585) },
                    { 6, "khtu2s", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7593), 5, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7594) },
                    { 7, "i0jtpq", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7600), 5, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7600) },
                    { 8, "rp8h0m", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7606), 3, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7606) },
                    { 9, "6whcmh", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7613), 4, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7614) },
                    { 10, "32qyft", new DateTime(2022, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7621), 3, new DateTime(2023, 12, 31, 19, 44, 2, 699, DateTimeKind.Utc).AddTicks(7621) }
                });

            migrationBuilder.InsertData(
                table: "BookGenres",
                columns: new[] { "SecondaryBooksId", "SecondaryGenresId" },
                values: new object[,]
                {
                    { 1, 5 },
                    { 4, 4 },
                    { 7, 1 },
                    { 11, 1 },
                    { 13, 5 },
                    { 17, 5 },
                    { 19, 3 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressId", "CouponCodeId", "CreatedAt", "PlacedDate", "State", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8428), new DateTime(2024, 11, 30, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8422), 1, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8430), 1 },
                    { 2, 7, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8560), new DateTime(2023, 6, 30, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8458), 3, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8561), 7 },
                    { 3, 8, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8571), new DateTime(2024, 5, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8566), 3, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8571), 8 },
                    { 4, 6, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8578), new DateTime(2024, 10, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8575), 0, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8578), 6 },
                    { 5, 8, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8584), new DateTime(2024, 8, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8582), 4, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8585), 8 },
                    { 6, 3, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8591), new DateTime(2023, 2, 28, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8589), 3, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8592), 3 },
                    { 7, 9, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8597), new DateTime(2024, 9, 30, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8596), 0, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8598), 9 },
                    { 8, 1, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8602), new DateTime(2024, 10, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8601), 0, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8603), 1 },
                    { 9, 5, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8608), new DateTime(2024, 3, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8606), 3, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8608), 5 },
                    { 10, 9, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8614), new DateTime(2023, 11, 30, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8612), 0, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8615), 9 },
                    { 11, 9, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8620), new DateTime(2023, 6, 30, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8618), 3, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8620), 9 },
                    { 12, 7, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8625), new DateTime(2024, 10, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8624), 1, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8626), 7 },
                    { 13, 1, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8630), new DateTime(2024, 3, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8629), 1, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8631), 1 },
                    { 14, 9, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8636), new DateTime(2024, 5, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8634), 4, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8636), 9 },
                    { 15, 4, null, new DateTime(2022, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8641), new DateTime(2023, 1, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8640), 3, 0m, new DateTime(2023, 12, 31, 19, 44, 2, 695, DateTimeKind.Utc).AddTicks(8642), 4 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "CreatedAt", "Note", "StarRating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7501), "Voluptas est ex expedita velit.\nIn autem ratione provident officia consequatur quia odio.", 1, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7502), 1 },
                    { 2, 2, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7591), "Facere nisi facilis ut vel placeat itaque et nihil dolore.\nNihil similique voluptas quibusdam accusamus aut non quisquam vero quia.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7591), 1 },
                    { 3, 3, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7634), "Iste ut enim consequatur numquam nemo voluptatem est.\nAut ut autem necessitatibus rerum tempore ex.", 1, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7634), 1 },
                    { 4, 2, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7692), "Et qui enim excepturi repudiandae ad tempora nobis.\nQuo necessitatibus modi et minima aut quaerat.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7693), 2 },
                    { 5, 3, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7725), "Non vero architecto.\nMolestiae alias iure explicabo non esse ullam ut.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7726), 2 },
                    { 6, 4, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7782), "Error explicabo animi quo voluptas ut omnis enim.\nNobis voluptatem voluptatem.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7783), 2 },
                    { 7, 3, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7822), "Odio est eum id odit velit.\nEnim molestias at ducimus ratione recusandae.", 3, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7823), 3 },
                    { 8, 4, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7869), "Beatae fuga ab consequatur eligendi eum harum modi consequuntur voluptas.\nDolores sunt magnam possimus et laboriosam laboriosam delectus.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7870), 3 },
                    { 9, 5, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7931), "Quibusdam doloremque aspernatur ea amet voluptatem consequatur eveniet eos.\nNeque veniam quaerat voluptatem accusantium pariatur perferendis.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7931), 3 },
                    { 10, 4, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7966), "Repellendus dolores unde.\nEaque non est id et.", 2, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7966), 4 },
                    { 11, 5, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7996), "Eveniet et quis harum vitae officia quam dicta.\nPerspiciatis totam qui est.", 1, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(7996), 4 },
                    { 12, 6, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8018), "Veniam quos ut aut sit facilis.\nEligendi voluptas ut.", 2, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8019), 4 },
                    { 13, 5, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8090), "Quod aperiam cupiditate voluptate cum enim facilis.\nQuibusdam voluptatum tempora modi.", 2, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8090), 5 },
                    { 14, 6, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8116), "Iste repellendus dolores quo sint dolore.\nTempore quis voluptate hic veniam eum.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8117), 5 },
                    { 15, 7, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8149), "Qui officiis ipsa voluptatibus suscipit error doloremque ea rerum.\nRem quo praesentium ut.", 3, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8150), 5 },
                    { 16, 6, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8301), "Eos quas rerum suscipit nihil consequatur aut.\nQui odio ea voluptatem laborum eum ducimus nihil vel nam.", 1, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8302), 6 },
                    { 17, 7, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8325), "Rerum quibusdam sunt omnis provident et.\nAb laudantium temporibus.", 2, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8325), 6 },
                    { 18, 8, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8357), "Tempore rerum sed aut.\nTotam fuga laboriosam praesentium fugiat perferendis exercitationem commodi a possimus.", 5, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8358), 6 },
                    { 19, 7, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8421), "Vero deserunt ipsam esse consequatur doloribus veritatis voluptatibus at.\nNihil repellat totam molestias fugiat in.", 3, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8422), 7 },
                    { 20, 8, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8458), "Est corporis delectus similique aut aspernatur ut est.\nQuos assumenda quos dolorum voluptatum voluptatem repudiandae praesentium voluptas nemo.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8459), 7 },
                    { 21, 9, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8485), "A eligendi animi adipisci et cupiditate.\nLaborum voluptatem qui dolorum unde.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8485), 7 },
                    { 22, 8, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8522), "Quis quo cum dolorem qui quis autem.\nDolor quas est assumenda ab.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8523), 8 },
                    { 23, 9, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8580), "Maiores aut quo eligendi sed voluptate non molestiae et aliquid.\nVoluptatem eaque similique dolorem velit.", 2, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8580), 8 },
                    { 24, 10, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8616), "Voluptatem quo et quae nam in officia numquam omnis cumque.\nNon natus ipsum eum sed esse rerum consequatur minus voluptas.", 3, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8616), 8 },
                    { 25, 9, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8649), "Ut repellendus sed libero quidem aut.\nNihil ut quo vel temporibus.", 1, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8649), 9 },
                    { 26, 10, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8707), "Voluptas aut impedit.\nOptio ut quo dolor temporibus iste quod totam sit aperiam.", 5, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8707), 9 },
                    { 27, 11, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8738), "Eaque officia atque illo harum ducimus reiciendis iusto temporibus.\nVitae beatae qui numquam magnam corrupti sed.", 2, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8738), 9 },
                    { 28, 10, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8777), "Voluptatem deleniti dolor quasi necessitatibus doloribus veritatis.\nCulpa quo eveniet consequuntur ut doloremque voluptatibus.", 4, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8777), 10 },
                    { 29, 11, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8798), "Veritatis minima dicta dolore totam sit est.\nEt architecto sunt enim.", 5, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8798), 10 },
                    { 30, 12, new DateTime(2022, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8856), "Suscipit totam enim.\nMollitia molestiae veritatis eligendi vel minus molestias impedit.", 1, new DateTime(2023, 12, 31, 19, 44, 2, 693, DateTimeKind.Utc).AddTicks(8856), 10 }
                });

            migrationBuilder.InsertData(
                table: "ShopItems",
                columns: new[] { "Id", "BookId", "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5419), 20.99m, 101, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5420) },
                    { 2, 2, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5424), 21.99m, 102, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5425) },
                    { 3, 3, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5427), 22.99m, 103, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5428) },
                    { 4, 4, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5430), 23.99m, 104, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5431) },
                    { 5, 5, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5433), 24.99m, 105, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5434) },
                    { 6, 6, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5437), 25.99m, 106, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5437) },
                    { 7, 7, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5439), 26.99m, 107, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5440) },
                    { 8, 8, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5442), 27.99m, 108, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5442) },
                    { 9, 9, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5445), 28.99m, 109, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5445) },
                    { 10, 10, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5448), 29.99m, 110, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5448) },
                    { 11, 11, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5451), 30.99m, 111, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5451) },
                    { 12, 12, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5453), 31.99m, 112, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5453) },
                    { 13, 13, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5456), 32.99m, 113, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5456) },
                    { 14, 14, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5458), 33.99m, 114, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5459) },
                    { 15, 15, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5462), 34.99m, 115, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5462) },
                    { 16, 16, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5464), 35.99m, 116, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5465) },
                    { 17, 17, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5467), 36.99m, 117, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5467) },
                    { 18, 18, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5470), 37.99m, 118, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5471) },
                    { 19, 19, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5473), 38.99m, 119, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5473) },
                    { 20, 20, new DateTime(2022, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5475), 39.99m, 120, new DateTime(2023, 12, 31, 19, 44, 2, 692, DateTimeKind.Utc).AddTicks(5476) }
                });

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "Id", "BookId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8659), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8661), 1 },
                    { 2, 2, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8669), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8669), 1 },
                    { 3, 2, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8682), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8682), 2 },
                    { 4, 3, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8684), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8685), 2 },
                    { 5, 3, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8695), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8695), 3 },
                    { 6, 4, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8698), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8699), 3 },
                    { 7, 4, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8709), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8709), 4 },
                    { 8, 5, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8711), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8712), 4 },
                    { 9, 5, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8722), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8722), 5 },
                    { 10, 6, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8747), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8748), 5 },
                    { 11, 6, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8757), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8758), 6 },
                    { 12, 7, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8760), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8760), 6 },
                    { 13, 7, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8769), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8770), 7 },
                    { 14, 8, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8772), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8773), 7 },
                    { 15, 8, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8782), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8782), 8 },
                    { 16, 9, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8785), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8785), 8 },
                    { 17, 9, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8796), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8796), 9 },
                    { 18, 10, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8799), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8799), 9 },
                    { 19, 10, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8810), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8810), 10 },
                    { 20, 11, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8813), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8813), 10 },
                    { 21, 11, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8848), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8849), 7 },
                    { 22, 9, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8856), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8856), 4 },
                    { 23, 19, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8862), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8862), 3 },
                    { 24, 10, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8869), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8869), 3 },
                    { 25, 2, new DateTime(2022, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8875), new DateTime(2023, 12, 31, 19, 44, 2, 694, DateTimeKind.Utc).AddTicks(8876), 10 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ShopItemId", "Count", "PricePerItem" },
                values: new object[,]
                {
                    { 1, 1, 2, 20.99m },
                    { 1, 2, 3, 21.99m },
                    { 2, 1, 5, 20.99m },
                    { 2, 2, 1, 21.99m },
                    { 2, 3, 2, 22.99m },
                    { 2, 4, 2, 23.99m },
                    { 3, 1, 1, 20.99m },
                    { 3, 2, 1, 21.99m },
                    { 3, 3, 2, 22.99m },
                    { 3, 4, 4, 23.99m },
                    { 4, 1, 1, 20.99m },
                    { 4, 2, 5, 21.99m },
                    { 4, 3, 5, 22.99m },
                    { 5, 1, 4, 20.99m },
                    { 5, 2, 2, 21.99m },
                    { 6, 1, 2, 20.99m },
                    { 6, 2, 1, 21.99m },
                    { 6, 3, 5, 22.99m },
                    { 7, 1, 3, 20.99m },
                    { 7, 2, 2, 21.99m },
                    { 7, 3, 5, 22.99m },
                    { 7, 4, 2, 23.99m },
                    { 7, 5, 2, 24.99m },
                    { 8, 1, 2, 20.99m },
                    { 8, 2, 3, 21.99m },
                    { 8, 3, 5, 22.99m },
                    { 8, 4, 1, 23.99m },
                    { 9, 1, 5, 20.99m },
                    { 9, 2, 4, 21.99m },
                    { 9, 3, 2, 22.99m },
                    { 9, 4, 2, 23.99m },
                    { 10, 1, 4, 20.99m },
                    { 10, 2, 2, 21.99m },
                    { 11, 1, 1, 20.99m },
                    { 11, 2, 5, 21.99m },
                    { 11, 3, 2, 22.99m },
                    { 11, 4, 2, 23.99m },
                    { 12, 1, 3, 20.99m },
                    { 12, 2, 5, 21.99m },
                    { 12, 3, 3, 22.99m },
                    { 12, 4, 4, 23.99m },
                    { 12, 5, 5, 24.99m },
                    { 13, 1, 3, 20.99m },
                    { 13, 2, 2, 21.99m },
                    { 13, 3, 1, 22.99m },
                    { 14, 1, 5, 20.99m },
                    { 14, 2, 2, 21.99m },
                    { 14, 3, 5, 22.99m },
                    { 14, 4, 1, 23.99m },
                    { 14, 5, 2, 24.99m },
                    { 15, 1, 3, 20.99m },
                    { 15, 2, 2, 21.99m },
                    { 15, 3, 2, 22.99m },
                    { 15, 4, 2, 23.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_SecondaryGenresId",
                table: "BookGenres",
                column: "SecondaryGenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponCodes_Code",
                table: "CouponCodes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CouponCodes_GiftCardId",
                table: "CouponCodes",
                column: "GiftCardId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ShopItemId",
                table: "OrderItems",
                column: "ShopItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponCodeId",
                table: "Orders",
                column: "CouponCodeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_BookId",
                table: "Ratings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopItems_BookId",
                table: "ShopItems",
                column: "BookId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_BookId",
                table: "Wishlists",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "CouponCodes");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GiftCards");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
