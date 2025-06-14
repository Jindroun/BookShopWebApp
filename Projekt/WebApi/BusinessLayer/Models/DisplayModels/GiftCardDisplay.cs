namespace BusinessLayer.Models.DisplayModels;

public class GiftCardDisplay : BaseDisplayEntity
{
    public decimal Discount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public List<CouponCodeSummary> CouponCodes { get; set; } = [];
}
