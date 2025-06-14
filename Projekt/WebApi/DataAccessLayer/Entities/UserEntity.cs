using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public record UserEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [JsonIgnore]
    public LocalIdentityUser AccountInfo { get; set; }
    [JsonIgnore]
    public List<AddressEntity> Addresses { get; set; }

    [JsonIgnore]
    public List<RatingEntity> Ratings { get; set; }
    [JsonIgnore]
    public List<WishlistEntryEntity> WishlistEntries { get; set; }
    [JsonIgnore]
    public List<OrderEntity> Orders { get; set; }

}
