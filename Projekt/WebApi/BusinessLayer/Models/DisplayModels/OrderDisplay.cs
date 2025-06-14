namespace BusinessLayer.Models.DisplayModels;
public class OrderDisplay : BaseDisplayEntity
{
    public string? UserFirstName { get; set; }
    public string? UserLastName { get; set; }
    public DateTime Date { get; set; }
    public string? State { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItemDisplay>? OrderItems { get; set; }
    public CouponCodeOrderDisplay? CouponCode { get; set; }
    public decimal FinalPrice => decimal.Max(TotalPrice - (CouponCode?.GiftCard.Discount ?? 0), 0);
}
