using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

public class OrdersViewModel
{
    public List<OrderDto>? Orders { get; set; }
    public List<ShopItemDisplay>? ShopItems { get; set; }

}
