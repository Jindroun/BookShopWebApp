using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public record BookEntity : BaseEntity
{
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    [JsonIgnore]
    public AuthorEntity Author { get; set; }
    public int PublisherId { get; set; }
    [JsonIgnore]
    public PublisherEntity Publisher { get; set; }
    public int GenreId { get; set; }
    [JsonIgnore]
    public GenreEntity Genre { get; set; }
    public List<GenreEntity> SecondaryGenres { get; set; } = [];
    public required string Isbn { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public List<WishlistEntryEntity> WishlistEntries { get; set; }

    [JsonIgnore]
    public ShopItemEntity ShopItem { get; set; }
}
