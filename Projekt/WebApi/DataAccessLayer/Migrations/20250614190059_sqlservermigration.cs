using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class sqlservermigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GiftCardId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
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
                    SecondaryBooksId = table.Column<int>(type: "int", nullable: false),
                    SecondaryGenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => new { x.SecondaryBooksId, x.SecondaryGenresId });
                    table.ForeignKey(
                        name: "FK_BookGenres_Books_SecondaryBooksId",
                        column: x => x.SecondaryBooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookGenres_Genres_SecondaryGenresId",
                        column: x => x.SecondaryGenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    StarRating = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    PlacedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CouponCodeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    ShopItemId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    PricePerItem = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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
                    { 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3874), "George", "Orwell", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3920) },
                    { 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3923), "J.R.R.", "Tolkien", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3923) },
                    { 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3925), "J.K.", "Rowling", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3925) },
                    { 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3927), "Stephen", "King", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3927) },
                    { 5, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3929), "Agatha", "Christie", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3929) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3960), "Science Fiction", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3961) },
                    { 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3964), "Fantasy", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3965) },
                    { 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3966), "Mystery", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3967) },
                    { 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3989), "Horror", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(3990) }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4013), "Penguin Random House", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4014) },
                    { 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4016), "HarperCollins", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4017) },
                    { 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4018), "Simon & Schuster", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4019) },
                    { 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4020), "Hachette Livre", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4020) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4065), "John", "Doe", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4066) },
                    { 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4073), "Jane", "Smith", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4074) },
                    { 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4075), "Alex", "Johnson", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4076) },
                    { 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4077), "Emily", "Davis", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4078) },
                    { 5, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4080), "Michael", "Brown", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4080) }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "PostalCode", "Street", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Springfield", "USA", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4098), "12345", "456 Oak Street", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4099), 1 },
                    { 2, "Rivertown", "USA", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4105), "23456", "789 Maple Avenue", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4105), 2 },
                    { 3, "Lakeside", "USA", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4107), "34567", "101 Pine Road", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4108), 3 },
                    { 4, "Greenfield", "USA", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4109), "98765", "202 Cedar Boulevard", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4110), 4 },
                    { 5, "Willowtown", "USA", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4111), "56789", "303 Birch Lane", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4112), 5 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserId", "UserName" },
                values: new object[,]
                {
                    { "2a7fd365-97c0-4ec0-a434-0e620428a37e", 0, "01488ffc-9263-4a9a-8981-a542e46d01b3", "jane.smith@example.com", false, false, null, null, null, null, null, false, "d474cd4d-af1c-4382-bedd-0b15d8bb8e3c", false, 2, "jane.smith@example.com" },
                    { "3d7b0b9c-5fac-4055-915e-af01f46bf526", 0, "624088cb-4aa5-42fe-bf79-66993decf3a0", "emily.davis@example.com", false, false, null, null, null, null, null, false, "9220cd8c-5868-4273-86b6-e2e55e678273", false, 4, "emily.davis@example.com" },
                    { "570c90fe-aef7-45c1-9b43-4cc6923c6ad5", 0, "385ce48a-4545-4add-b173-7d6bfc129711", "john.doe@example.com", false, false, null, null, null, null, null, false, "5b420cc0-ed41-472c-8c77-c13f52f7a3d1", false, 1, "john.doe@example.com" },
                    { "5f7b8d3e-9833-44d1-b24c-8a9ec4a2da71", 0, "95e65a99-b0fb-45f4-83a4-ceb5d1d6295b", "michael.brown@example.com", false, false, null, null, null, null, null, false, "c8d4bb84-c122-424d-b74b-63976bd27476", false, 5, "michael.brown@example.com" },
                    { "fd2186e7-06f3-4c6e-8484-12b3b29ec79e", 0, "882d58a5-4d5c-465a-99ed-ef6ee9556b00", "alex.johnson@example.com", false, false, null, null, null, null, null, false, "914bb1c2-0f0a-4d2b-9f7b-6e67ac394cb1", false, 3, "alex.johnson@example.com" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Description", "GenreId", "Isbn", "PublisherId", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4036), "A dystopian novel set in a totalitarian regime, where Big Brother watches over all citizens.", 1, "9780451524935", 1, "1984", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4037) },
                    { 2, 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4042), "An epic fantasy trilogy following the journey to destroy the One Ring and defeat the dark lord Sauron.", 2, "9780618640157", 2, "The Lord of the Rings", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4043) },
                    { 3, 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4045), "The first book in the Harry Potter series, introducing the young wizard and his journey at Hogwarts.", 2, "9780747532743", 3, "Harry Potter and the Philosopher's Stone", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4045) },
                    { 4, 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4047), "A psychological horror novel about a family's terrifying experience in a haunted hotel.", 4, "9780307743657", 4, "The Shining", new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4047) }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressId", "CouponCodeId", "CreatedAt", "PlacedDate", "State", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4369), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 40m, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4370), 1 },
                    { 2, 2, null, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4376), new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 48m, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4377), 2 },
                    { 3, 2, null, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4379), new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 0m, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4379), 2 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "BookId", "CreatedAt", "Note", "StarRating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4130), "A chilling depiction of a dystopian future.", 5, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4131), 1 },
                    { 2, 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4136), "An epic journey filled with lore and adventure.", 5, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4137), 2 },
                    { 3, 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4138), "A magical beginning to an iconic series.", 5, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4139), 3 },
                    { 4, 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4140), "A terrifying descent into madness.", 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4141), 4 },
                    { 5, 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4142), "Thought-provoking and eerily relevant.", 5, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4143), 5 }
                });

            migrationBuilder.InsertData(
                table: "ShopItems",
                columns: new[] { "Id", "BookId", "CreatedAt", "Price", "Stock", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4186), 10m, 100, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4187) },
                    { 2, 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4194), 20m, 50, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4194) },
                    { 3, 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4196), 15m, 75, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4197) },
                    { 4, 4, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4198), 12m, 0, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4199) }
                });

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "Id", "BookId", "CreatedAt", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4162), new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4163), 1 },
                    { 2, 2, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4166), new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4167), 1 },
                    { 3, 1, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4168), new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4169), 2 },
                    { 4, 3, new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4170), new DateTime(2025, 6, 14, 21, 0, 58, 992, DateTimeKind.Local).AddTicks(4171), 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderId", "ShopItemId", "Count", "PricePerItem" },
                values: new object[,]
                {
                    { 1, 1, 2, 10m },
                    { 1, 2, 1, 20m },
                    { 2, 3, 3, 16m }
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
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                unique: true,
                filter: "[CouponCodeId] IS NOT NULL");

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
