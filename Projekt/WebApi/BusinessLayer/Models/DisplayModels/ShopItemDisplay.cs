namespace BusinessLayer.Models.DisplayModels;
public class ShopItemDisplay : BaseDisplayEntity
{
    public int BookId { get; set; }
    public string? BookTitle { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string? AuthorFirstName { get; set; }
    public string? AuthorLastName { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
}
