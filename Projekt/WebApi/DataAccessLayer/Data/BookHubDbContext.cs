using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text;

namespace DataAccessLayer.Data
{
    public class BookHubDbContext : IdentityDbContext<LocalIdentityUser>
    {
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<WishlistEntryEntity> Wishlists { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
        public DbSet<ShopItemEntity> ShopItems { get; set; }
        public DbSet<LocalIdentityUser> LocalIdentities { get; set; }
        public DbSet<AuditLogEntity> AuditLogs { get; set; }
        public DbSet<GiftCardEntity> GiftCards { get; set; }
        public DbSet<CouponCodeEntity> CouponCodes { get; set; }
        public DbSet<BookGenreEntity> BookGenres { get; set; } // secondary, only for seeding

        public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookEntity>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookEntity>()
                .HasMany(b => b.SecondaryGenres)
                .WithMany(g => g.SecondaryBooks)
                .UsingEntity<BookGenreEntity>();

            modelBuilder.Entity<BookEntity>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookEntity>()
                .HasIndex(b => b.Isbn)
                .IsUnique();

            modelBuilder.Entity<RatingEntity>()
                .HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Addresses)
                .WithOne(a => a.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AddressEntity>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WishlistEntryEntity>()
               .HasOne(we => we.Book)
               .WithMany(b => b.WishlistEntries)
               .HasForeignKey(we => we.BookId);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(o => o.Address)
                .WithMany();

            modelBuilder.Entity<OrderEntity>()
                .HasMany(o => o.ShopItems)
                .WithMany(si => si.Orders)
                .UsingEntity<OrderItemEntity>();

            modelBuilder.Entity<ShopItemEntity>()
                .HasOne(si => si.Book)
                .WithOne(b => b.ShopItem)
                .HasForeignKey<ShopItemEntity>(si => si.BookId);

            modelBuilder.Entity<LocalIdentityUser>()
                .HasOne(u => u.User)
                .WithOne(r => r.AccountInfo)
                .HasForeignKey<LocalIdentityUser>(u => u.UserId);

            DataInitializer.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            WriteAuditLog();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void WriteAuditLog()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
                .ToList();
            foreach (var entry in entries.Where(e => e.Metadata.FindProperty("Id") != null))
            {
                var entity = entry.Entity;
                var auditLog = new AuditLogEntity
                {
                    UserId = 1, // https://discord.com/channels/1145999650877349909/1146003447360008253/1308542272597528577
                    Action = entry.State.ToString(),
                    EntityName = entry.Entity.GetType().Name,
                    Timestamp = DateTime.Now,
                    EntityId = entry.Property("Id").CurrentValue?.ToString() ?? "N/A",
                    Changes = GetChanges(entry)
                };
                AuditLogs.Add(auditLog);
            }
        }

        private static string GetChanges(EntityEntry entry)
        {
            var changes = new StringBuilder();
            foreach (var property in entry.OriginalValues.Properties)
            {
                var originalValue = entry.OriginalValues[property];
                var currentValue = entry.CurrentValues[property];
                if (!Equals(originalValue, currentValue))
                {
                    changes.AppendLine($"{property.Name}: '{originalValue}' -> '{currentValue}'");
                }
            }
            return changes.ToString();
        }
    }
}
