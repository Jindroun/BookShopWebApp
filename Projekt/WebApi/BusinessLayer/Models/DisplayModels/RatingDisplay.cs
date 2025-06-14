namespace BusinessLayer.Models.DisplayModels;
public class RatingDisplay : BaseDisplayEntity
{
    public int? StarRating { get; set; }
    public string? Note { get; set; }
    public int UserId { get; set; }
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }
    public int BookId { get; set; }
    public string? BookTitle { get; set; }
}
