using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.InputModels;
public class RatingDto
{
    public int UserId { get; set; } // The ID of the user making the rating
    public int BookId { get; set; } // The ID of the book being rated

    [Range(0, 5, ErrorMessage = "Star rating must be between 0 and 5.")]
    public int StarRating { get; set; } // Star rating (1-5)
    public string? Note { get; set; } // Optional note about the rating
}
