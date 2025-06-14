namespace DataAccessLayer.Entities;
public record PublisherEntity : BaseEntity
{
    public required string Name { get; set; }
    public List<BookEntity> Books { get; set; }
}
