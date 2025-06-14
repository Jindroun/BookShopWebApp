namespace BusinessLayer.Models.DisplayModels;

public class CouponCodeDisplay : BaseDisplayEntity
{
    public required string Code { get; set; }
    public required GiftCardSummary GiftCard { get; set; }
    public OrderDisplay? Order { get; set; }
}
