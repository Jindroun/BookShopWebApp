namespace BusinessLayer.Models.DisplayModels;

public class GiftCardSummary : BaseDisplayEntity
{
    public decimal Discount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int CouponCodeCount;
}
