namespace BusinessLayer;
public static class CacheKeys
{
    public static string GetShopItemsPageKey(int page) => $"shopItemsPage{page}";
    public static string GetShopItemKey(int shopItemId) => $"shopItem{shopItemId}";
    public static string GetUserOrdersKey(string userId) => $"orders{userId}";
    public static string GetShopItemByBookKey(string bookId) => $"shopItemByBook{bookId}";
}
