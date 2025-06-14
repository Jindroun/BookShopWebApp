using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations
{
    public class WishlistService : IWishlistService
    {
        private readonly IMapper mapper;
        private readonly BookHubDbContext dbContext;

        public WishlistService(IMapper mapper, BookHubDbContext context)
        {
            this.mapper = mapper;
            dbContext = context;
        }

        public IQueryable<WishlistEntryDisplay> GetWishlistEntries()
        {
            return dbContext.Wishlists
                .Include(r => r.User)
                .Include(r => r.Book)
                .ProjectTo<WishlistEntryDisplay>(mapper.ConfigurationProvider);
        }

        public async Task<OperationResult> DeleteWishlistEntry(int wishlistEntryId)
        {
            try
            {
                var wishlistEntry = await dbContext.Wishlists.FindAsync(wishlistEntryId);

                if (wishlistEntry == null)
                {
                    return OperationResult.Failure("Wishlist entry not found.");
                }

                dbContext.Wishlists.Remove(wishlistEntry);
                await dbContext.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred when deleting the wishlist entry.", ex);
            }
        }

        public async Task<OperationResult<WishlistEntryDisplay>> AddWishlistEntry(WishlistEntryDto wishlistEntryDto)
        {
            try
            {
                // Consolidate user and book validation into a single query
                var userExists = await dbContext.Users.AnyAsync(u => u.Id == wishlistEntryDto.UserId);
                if (!userExists)
                {
                    return OperationResult<WishlistEntryDisplay>.Failure("User not found.");
                }

                var bookExists = await dbContext.Books.AnyAsync(b => b.Id == wishlistEntryDto.BookId);
                if (!bookExists)
                {
                    return OperationResult<WishlistEntryDisplay>.Failure("Book not found.");
                }

                // Create the new wishlist entry entity
                var newWishlistEntry = mapper.Map<WishlistEntryEntity>(wishlistEntryDto);
                newWishlistEntry.CreatedAt = DateTime.Now;

                await dbContext.Wishlists.AddAsync(newWishlistEntry);
                await dbContext.SaveChangesAsync();

                var wishlistDtoResult = mapper.Map<WishlistEntryDisplay>(newWishlistEntry);
                return OperationResult<WishlistEntryDisplay>.Success(wishlistDtoResult);
            }
            catch (Exception ex)
            {
                return OperationResult<WishlistEntryDisplay>.Failure("An error occurred while adding the wishlist entry.", ex);
            }
        }
    }
}
