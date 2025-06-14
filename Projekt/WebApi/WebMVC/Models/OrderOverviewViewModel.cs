using BusinessLayer.Models.DisplayModels;

namespace WebMVC.Models;

public class OrderOverviewViewModel
{
    public List<OrderDisplay> Orders { get; set; } = new List<OrderDisplay>();
}
