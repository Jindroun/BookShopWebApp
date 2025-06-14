namespace BusinessLayer.Models.DisplayModels;

public class CouponCodeOrderDisplay : BaseDisplayEntity
{
    public required string Code { get; set; }
    public required GiftCardSummary GiftCard { get; set; }
}
