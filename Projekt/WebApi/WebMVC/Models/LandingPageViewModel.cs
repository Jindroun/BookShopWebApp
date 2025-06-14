using BusinessLayer.Models.DisplayModels;

namespace WebMVC.Models
{
    public class LandingPageViewModel
    {
        public List<ShopItemDisplay> ShopItems { get; set; } = new List<ShopItemDisplay>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
    }
}
