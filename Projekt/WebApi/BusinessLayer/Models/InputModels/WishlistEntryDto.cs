namespace BusinessLayer.Models.InputModels;
public class WishlistEntryDto
{
    public int UserId { get; set; } // The ID of the user making the rating
    public int BookId { get; set; } // The ID of the book being rated

}
