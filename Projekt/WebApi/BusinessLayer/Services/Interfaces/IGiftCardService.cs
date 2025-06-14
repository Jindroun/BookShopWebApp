using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;

public interface IGiftCardService 
{
    public Task<IEnumerable<GiftCardDisplay>> GetGiftCardsAsync();
    public Task<OperationResult<GiftCardDisplay>> GetGiftCardAsync(int id);
    public Task<OperationResult<GiftCardDisplay>> AddGiftCardAsync(GiftCardDto giftCardDto);
    public Task<OperationResult> DeleteGiftCardAsync(int id);
}
