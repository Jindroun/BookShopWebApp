using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class GiftCardService : IGiftCardService
{
    private readonly BookHubDbContext dbContext;
    private readonly IMapper mapper;
    private static readonly string codeChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static readonly Random random = new Random();

    public GiftCardService(BookHubDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    private async Task<string> GenerateCouponCode(int len = 8)
    {
        string? code;
        while (true) {
            code = string.Concat(Enumerable.Range(0, len).Select(_ => codeChars[random.Next(codeChars.Length)]));
            var existingCouponCode = await dbContext.CouponCodes
                .FirstOrDefaultAsync(cc => cc.Code == code);
            if (existingCouponCode == null) {
                break;
            }
        }
        return code;
    }

    public async Task<OperationResult<GiftCardDisplay>> AddGiftCardAsync(GiftCardDto giftCardDto)
    {
        try 
        {
            var giftCard = mapper.Map<GiftCardEntity>(giftCardDto);

            // generate coupon codes
            for (int i = 0; i < giftCardDto.CouponCodeCount; i++)
            {
                var couponCode = new CouponCodeEntity
                {
                    Code = await GenerateCouponCode(),
                };
                giftCard.CouponCodes.Add(couponCode);
            }

            dbContext.GiftCards.Add(giftCard);
            await dbContext.SaveChangesAsync();

            var giftCardResult = mapper.Map<GiftCardDisplay>(giftCard);
            return OperationResult<GiftCardDisplay>.Success(giftCardResult);
        }
        catch (Exception ex)
        {
            return OperationResult<GiftCardDisplay>.Failure("An error occurred while adding the gift card.", ex);
        }
    }

    public async Task<OperationResult> DeleteGiftCardAsync(int id)
    {
        try 
        {
            var giftCard = await dbContext.GiftCards.FindAsync(id);
            if (giftCard == null)
            {
                return OperationResult.Failure("Gift card not found.");
            }

            dbContext.GiftCards.Remove(giftCard);
            await dbContext.SaveChangesAsync();
            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            return OperationResult.Failure("An error occurred while deleting the gift card.", ex);
        }
    }


    public async Task<OperationResult<GiftCardDisplay>> GetGiftCardAsync(int id)
    {
        try
        {
            var giftCard = await dbContext.GiftCards
                .AsNoTracking()
                .Include(gc => gc.CouponCodes)
                .SingleOrDefaultAsync(gc => gc.Id == id);
            if (giftCard == null)
            {
                return OperationResult<GiftCardDisplay>.Failure("Gift card not found.");
            }

            var giftCardResult = mapper.Map<GiftCardDisplay>(giftCard);
            return OperationResult<GiftCardDisplay>.Success(giftCardResult);
        }
        catch (Exception ex)
        {
            return OperationResult<GiftCardDisplay>.Failure("An error occurred while getting the gift card.", ex);
        }
    }

    public async Task<IEnumerable<GiftCardDisplay>> GetGiftCardsAsync()
    {
        return await dbContext.GiftCards
            .AsNoTracking()
            .Include(gc => gc.CouponCodes)
            .ProjectTo<GiftCardDisplay>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
