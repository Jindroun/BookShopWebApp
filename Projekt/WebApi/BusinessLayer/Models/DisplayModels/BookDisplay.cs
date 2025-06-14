namespace BusinessLayer.Models.DisplayModels;
public class BookDisplay : BaseDisplayEntity
{
    public required string Title { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public string Publisher { get; set; }
    public string Genre { get; set; }
    public List<string> SecondaryGenres { get; set; } = [];
    public required string Isbn { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public string Stock { get; set; }
}
