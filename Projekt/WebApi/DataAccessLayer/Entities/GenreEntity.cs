namespace DataAccessLayer.Entities;
public record GenreEntity : BaseEntity
{
    public required string Name { get; set; }
    public List<BookEntity> Books { get; set; }
    public List<BookEntity> SecondaryBooks { get; set; } = [];
}
