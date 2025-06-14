namespace BusinessLayer.Models.DisplayModels;
public class OrderItemDisplay
{
    public int ShopItemId { get; set; }
    public int Count { get; set; }
    public decimal PricePerItem { get; set; }

    public string BookTitle { get; set; }
}
