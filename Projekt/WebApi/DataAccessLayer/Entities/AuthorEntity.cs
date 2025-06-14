namespace DataAccessLayer.Entities;

public record AuthorEntity : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<BookEntity> Books { get; set; }
}
