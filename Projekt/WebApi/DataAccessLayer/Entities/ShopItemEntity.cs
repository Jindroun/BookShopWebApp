using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public record ShopItemEntity : BaseEntity
{
    public int BookId { get; set; }
    [JsonIgnore]
    public BookEntity Book { get; set; }

    public int Stock { get; set; }
    public decimal Price { get; set; }
    [JsonIgnore]
    public List<OrderEntity> Orders { get; set; }
    [JsonIgnore]
    public List<OrderItemEntity> OrderItems { get; set; }
}
