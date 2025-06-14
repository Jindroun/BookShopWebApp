namespace BusinessLayer.Models.InputModels;

public class GiftCardDto
{
    public decimal Discount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int CouponCodeCount { get; set; } = 0;
}
