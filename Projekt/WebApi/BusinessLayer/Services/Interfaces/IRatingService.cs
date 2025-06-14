using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;
public interface IRatingService
{
    public IQueryable<RatingDisplay> GetRatings();
    public Task<OperationResult> DeleteRating(int RatingId);
    public Task<OperationResult<RatingDisplay>> AddRating(RatingDto ratingDto);
    public Task<OperationResult<RatingDisplay>> UpdateRating(int ratingId, RatingDto updatedRatingDto);

}
