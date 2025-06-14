using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public record RatingEntity:BaseEntity
{
    public int UserId { get; set; }
    [JsonIgnore]
    public UserEntity User { get; set; }
    public int BookId { get; set; }
    [JsonIgnore]
    public BookEntity Book { get; set; }
    public int StarRating { get; set; }
    public string Note { get; set; }
}
