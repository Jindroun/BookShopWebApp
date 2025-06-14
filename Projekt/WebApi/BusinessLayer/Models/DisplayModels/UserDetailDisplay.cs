namespace BusinessLayer.Models.DisplayModels;
public class UserDetailDisplay : BaseDisplayEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public AddressDisplay? Address { get; set; } 

    public List<RatingDisplay> Ratings { get; set; }
    public List<WishlistEntryDisplay> WishlistEntries { get; set; }

    //ORDERS TO BE ADDED
}