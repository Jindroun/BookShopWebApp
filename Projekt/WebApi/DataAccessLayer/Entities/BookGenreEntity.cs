namespace DataAccessLayer.Entities;

// only for seeding
public record BookGenreEntity
{
    public int SecondaryBooksId { get; set; }
    public int SecondaryGenresId { get; set; }
}