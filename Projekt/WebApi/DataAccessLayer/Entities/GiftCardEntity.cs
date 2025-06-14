namespace DataAccessLayer.Entities;
public record GiftCardEntity : BaseEntity
{
    public decimal Discount { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public List<CouponCodeEntity> CouponCodes { get; set; } = [];
}
