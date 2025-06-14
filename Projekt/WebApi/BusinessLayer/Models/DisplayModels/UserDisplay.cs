namespace BusinessLayer.Models.DisplayModels;
public class UserDisplay : BaseDisplayEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Street { get; set; }

    public required string City { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }


}
