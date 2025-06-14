using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public record OrderEntity : BaseEntity
{
    public int UserId { get; set; }
    [JsonIgnore]
    public UserEntity User { get; set; }

    public int AddressId { get; set; }
    [JsonIgnore]
    public AddressEntity Address { get; set; }

    public DateTime PlacedDate { get; set; }
    public OrderState State { get; set; }
    public decimal TotalPrice { get; set; }
    [JsonIgnore]
    public List<ShopItemEntity> ShopItems { get; set; }
    [JsonIgnore]
    public List<OrderItemEntity> OrderItems { get; set; }
    public int? CouponCodeId { get; set; }
    [JsonIgnore]
    public CouponCodeEntity? CouponCode { get; set; }
}

public enum OrderState
{
    Unpaid,
    Paid,
    Sent,
    Fullfilled,
    Cancelled
}