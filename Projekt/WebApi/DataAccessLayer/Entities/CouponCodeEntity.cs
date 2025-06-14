using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

[Index(nameof(Code), IsUnique = true)]
public record CouponCodeEntity : BaseEntity
{
    public required string Code { get; set; }
    public int GiftCardId { get; set; }
    public GiftCardEntity GiftCard { get; set; }
    public OrderEntity? Order { get; set; }
}
