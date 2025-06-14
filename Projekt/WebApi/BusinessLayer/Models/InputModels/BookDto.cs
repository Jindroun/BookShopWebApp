namespace BusinessLayer.Models.InputModels;
public class BookDto
{
    public int? Id { get; set; }
    public required string Title { get; set; }
    public required int AuthorId { get; set; }
    public required int PublisherId { get; set; }
    public required int GenreId { get; set; }
    public required string Isbn { get; set; }
    public string Description { get; set; }
}   
