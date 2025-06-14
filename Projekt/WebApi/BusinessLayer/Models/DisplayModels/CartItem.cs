namespace BusinessLayer.Models.DisplayModels;
public class CartItem
{
    public string? BookTitle { get; set; }
    public decimal Price { get; set; }

    public int ShopItemId { get; set; }
    public int Count { get; set; }
}
