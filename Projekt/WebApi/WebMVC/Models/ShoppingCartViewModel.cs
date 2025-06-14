using BusinessLayer.Models.DisplayModels;

namespace WebMVC.Models;

public class ShoppingCartViewModel
{
    public List<CartItem> CartItems { get; set; } = [];
    public CouponCodeDisplay? CouponCode { get; set; }
    public string CouponInfoMessage { get; set; } = "";
    public decimal Total => CartItems?.Sum(item => item.Price * item.Count) ?? 0;

    public string FinalPrice => CouponCode == null ?
        Total.ToString() :
        $"{Total} - {CouponCode.GiftCard.Discount} = {decimal.Max(Total - CouponCode.GiftCard.Discount, 0)}";
}
