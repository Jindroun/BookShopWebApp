using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;
public interface IShopItemService
{
    public IQueryable<ShopItemDisplay> GetShopItems();
    public Task<OperationResult> DeleteShopItem(int shopItemId, bool force = false);
    public Task<OperationResult<ShopItemDisplay>> AddShopItem(ShopItemDto shopItemDto);
    public Task<OperationResult<ShopItemDisplay>> UpdateShopItem(int shopItemId, ShopItemDto updatedShopItemDto);
    public Task<PaginatedResult<ShopItemDisplay>> GetShopItemsByPage(int page, int itemsPerPage);
}
