using AutoMapper;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class CouponCodeService : ICouponCodeService 
{
    private readonly BookHubDbContext dbContext;
    private readonly IMapper mapper;

    public CouponCodeService(BookHubDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    
    public async Task<OperationResult<CouponCodeDisplay>> GetCouponCodeAsync(int id)
    {
        try
        {
            var couponCode = await dbContext.CouponCodes
                .AsNoTracking()
                .Include(cc => cc.GiftCard)
                .Include(cc => cc.Order)
                .ThenInclude(o => o!.User)
                .FirstOrDefaultAsync(cc => cc.Id == id);
            if (couponCode == null)
            {
                return OperationResult<CouponCodeDisplay>.Failure("Coupon code not found.");
            }

            var couponCodeResult = mapper.Map<CouponCodeDisplay>(couponCode);
            return OperationResult<CouponCodeDisplay>.Success(couponCodeResult);
        }
        catch (Exception ex)
        {
            return OperationResult<CouponCodeDisplay>.Failure("An error occurred while getting the coupon code.", ex);
        }
    }

    public async Task<OperationResult<CouponCodeDisplay>> GetCouponCodeAsync(string code)
    {
        try
        {
            var couponCode = await dbContext.CouponCodes
                .AsNoTracking()
                .Include(cc => cc.GiftCard)
                .Include(cc => cc.Order)
                .ThenInclude(o => o!.User)
                .FirstOrDefaultAsync(cc => cc.Code == code);
            if (couponCode == null)
            {
                return OperationResult<CouponCodeDisplay>.Failure("Coupon code not found.");
            }

            var couponCodeResult = mapper.Map<CouponCodeDisplay>(couponCode);
            return OperationResult<CouponCodeDisplay>.Success(couponCodeResult);
        }
        catch (Exception ex)
        {
            return OperationResult<CouponCodeDisplay>.Failure("An error occurred while getting the coupon code.", ex);
        }
    }

    public async Task<OperationResult<CouponCodeSummary>> AddCouponCodeAsync(CouponCodeDto couponCodeDto)
    {
        try {
            var giftCard = await dbContext.GiftCards.FindAsync(couponCodeDto.GiftCardId);
            if (giftCard == null)
            {
                return OperationResult<CouponCodeSummary>.Failure("Gift card not found.");
            }

            var existingCouponCode = await dbContext.CouponCodes
                .FirstOrDefaultAsync(cc => cc.Code == couponCodeDto.Code);
            if (existingCouponCode != null)
            {
                return OperationResult<CouponCodeSummary>.Failure("Coupon with the same code already exists.");
            }

            var couponCode = mapper.Map<CouponCodeEntity>(couponCodeDto);
            dbContext.CouponCodes.Add(couponCode);
            await dbContext.SaveChangesAsync();

            var couponCodeResult = mapper.Map<CouponCodeSummary>(couponCode);
            return OperationResult<CouponCodeSummary>.Success(couponCodeResult);
        }
        catch (Exception ex)
        {
            return OperationResult<CouponCodeSummary>.Failure("An error occurred while adding the coupon code.", ex);
        }
    }

    public async Task<OperationResult> DeleteCouponCodeAsync(int id)
    {
        try 
        {
            var couponCode = await dbContext.CouponCodes.FindAsync(id);
            if (couponCode == null)
            {
                return OperationResult.Failure("Coupon code not found.");
            }

            dbContext.CouponCodes.Remove(couponCode);
            await dbContext.SaveChangesAsync();
            return OperationResult.Success();
        }
        catch (Exception ex)
        {
            return OperationResult.Failure("An error occurred while deleting the coupon code.", ex);
        }
    }
}
