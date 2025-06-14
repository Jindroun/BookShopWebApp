using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;
public interface IWishlistService
{
    public IQueryable<WishlistEntryDisplay> GetWishlistEntries();
    public Task<OperationResult> DeleteWishlistEntry(int WishlistEntryId);
    public Task<OperationResult<WishlistEntryDisplay>> AddWishlistEntry(WishlistEntryDto wishlistEntryDto);

}
