using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public record OrderItemEntity
{
    public int ShopItemId { get; set; }
    [JsonIgnore]
    public ShopItemEntity ShopItem { get; set; }
    public int OrderId { get; set; }
    [JsonIgnore]
    public OrderEntity Order { get; set; }
    public int Count { get; set; }
    public decimal PricePerItem { get; set; }
}
