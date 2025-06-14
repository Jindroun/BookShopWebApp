namespace BusinessLayer.Models.DisplayModels;

public class CouponCodeSummary : BaseDisplayEntity
{
    public required string Code { get; set; }
    public int GiftCardId { get; set; }
    public int? OrderId { get; set; }
}
