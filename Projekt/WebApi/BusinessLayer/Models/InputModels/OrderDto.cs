namespace BusinessLayer.Models.InputModels;

public class OrderDto
{
    public int? Id { get; set; }
    public int UserId { get; set; }
    public AddressDto? Address { get; set; } // if null, user's default address is used
    public List<OrderItemDto> OrderItems { get; set; } = [];
    public string? CouponCode { get; set; }
}
