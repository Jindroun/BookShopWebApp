namespace BusinessLayer.Models.InputModels;
public class BookSearchAttributes
{
    public string? Genre { get; set; }
    public string? Author { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
