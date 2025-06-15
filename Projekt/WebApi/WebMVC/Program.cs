using BusinessLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using WebMVC;
using WebMVC.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BookHubDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null);
        }), 
        ServiceLifetime.Scoped);

/*
builder.Services.AddDbContext<BookHubDbContext>(options =>
{
    var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    var dbName = "bookhub.db";
    var dbPath = Path.Combine(appDataPath, dbName);
    options.UseSqlite($"Data Source={dbPath}");
});
*/
builder.Services.AddServices();
builder.Services.AddAutoMapper(typeof(AutoMapperMvcProfile));

// This line adds the default identity system configuration for the specified user and role types to the services container.
builder.Services.AddIdentity<LocalIdentityUser, IdentityRole>()
    // This method call specifies that the identity system will use Entity Framework to store and manage user information,
    // with 'SeminarDBContext' as the database context class that handles the connection to the database.
    .AddEntityFrameworkStores<BookHubDbContext>()
    // This adds the default token providers used to generate tokens for account confirmation, password resets, etc.
    .AddDefaultTokenProviders();

async Task SeedRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
}

// Configure Identity options for password policies
builder.Services.Configure<IdentityOptions>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    // Set the login path for unauthenticated users
    options.LoginPath = "/Account/Login";

    // Set the expiration time of the cookie based on RememberMe
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Default session expiration (30 minutes)

    options.SlidingExpiration = true;  // Makes the cookie expiration extend if the user is active

    // Cookie settings for securing the cookie
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;  // Ensure cookie security in production

    // If the user selects "Remember me", extend the expiration time
    options.Cookie.MaxAge = TimeSpan.FromDays(14);  // 14 days for persistent sessions
});

builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
    options.Cookie.HttpOnly = true; // Secure the cookie
});

var app = builder.Build();

// Seed roles after building the app
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRoles(services);
}
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<BookHubDbContext>();
    db.Database.Migrate(); // 🆕 Apply EF Core Migrations

    await SeedRoles(services); // Existing role seeding

}

app.Run();
