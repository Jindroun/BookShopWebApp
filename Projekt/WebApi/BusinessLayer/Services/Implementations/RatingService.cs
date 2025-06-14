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
    public class RatingService : IRatingService
    {
        private readonly IMapper mapper;
        private readonly BookHubDbContext dbContext;

        public RatingService(IMapper mapper, BookHubDbContext context)
        {
            this.mapper = mapper;
            dbContext = context;
        }

        public IQueryable<RatingDisplay> GetRatings()
        {
            return dbContext.Ratings
                .Include(r => r.User)
                .Include(r => r.Book)
                .ProjectTo<RatingDisplay>(mapper.ConfigurationProvider);
        }

        public async Task<OperationResult> DeleteRating(int RatingId)
        {
            try
            {
                var rating = await dbContext.Ratings.FindAsync(RatingId);

                if (rating == null)
                {
                    return OperationResult.Failure("Rating not found.");
                }

                dbContext.Ratings.Remove(rating);
                await dbContext.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred when deleting the rating.", ex);
            }
        }

        public async Task<OperationResult<RatingDisplay>> AddRating(RatingDto ratingDto)
        {
            try
            {
                // Validate the user and book in a single query
                var user = await dbContext.Users.FindAsync(ratingDto.UserId);
                var book = await dbContext.Books.FindAsync(ratingDto.BookId);

                if (user == null)
                {
                    return OperationResult<RatingDisplay>.Failure("User not found.");
                }

                if (book == null)
                {
                    return OperationResult<RatingDisplay>.Failure("Book not found.");
                }

                // Create the new rating entity
                var newRating = mapper.Map<RatingEntity>(ratingDto);
                newRating.CreatedAt = DateTime.Now;

                dbContext.Ratings.Add(newRating);
                await dbContext.SaveChangesAsync();

                var ratingDtoResult = mapper.Map<RatingDisplay>(newRating);
                return OperationResult<RatingDisplay>.Success(ratingDtoResult);
            }
            catch (Exception ex)
            {
                return OperationResult<RatingDisplay>.Failure("An error occurred while adding the rating.", ex);
            }
        }

        public async Task<OperationResult<RatingDisplay>> UpdateRating(int ratingId, RatingDto updatedRatingDto)
        {
            try
            {
                // Validate the user and book
                var user = await dbContext.Users.FindAsync(updatedRatingDto.UserId);
                var book = await dbContext.Books.FindAsync(updatedRatingDto.BookId);

                if (user == null)
                {
                    return OperationResult<RatingDisplay>.Failure("User not found.");
                }

                if (book == null)
                {
                    return OperationResult<RatingDisplay>.Failure("Book not found.");
                }

                // Fetch the existing rating with related entities
                var existingRating = await dbContext.Ratings
                    .Include(r => r.Book)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(u => u.Id == ratingId);

                if (existingRating == null)
                {
                    return OperationResult<RatingDisplay>.Failure("Rating not found.");
                }

                // Update only the properties from the DTO
                mapper.Map(updatedRatingDto, existingRating);
                existingRating.UpdatedAt = DateTime.Now;

                await dbContext.SaveChangesAsync();

                var ratingDtoResult = mapper.Map<RatingDisplay>(existingRating);
                return OperationResult<RatingDisplay>.Success(ratingDtoResult);
            }
            catch (Exception ex)
            {
                return OperationResult<RatingDisplay>.Failure("An error occurred while updating the rating.", ex);
            }
        }
    }
}
