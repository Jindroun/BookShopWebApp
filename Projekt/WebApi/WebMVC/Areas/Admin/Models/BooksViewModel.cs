using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

public class BooksViewModel
{
    public List<BookDto>? Books { get; set; }
    public List<AuthorDisplay>? Authors { get; set; }
    public List<PublisherDisplay>? Publishers { get; set; }
    public List<GenreDisplay>? Genres { get; set; }
}
