using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;

namespace BusinessLayer.Services.Interfaces;

public interface ICouponCodeService
{
    public Task<OperationResult<CouponCodeSummary>> AddCouponCodeAsync(CouponCodeDto couponCodeDto);
    public Task<OperationResult<CouponCodeDisplay>> GetCouponCodeAsync(int id);
    public Task<OperationResult<CouponCodeDisplay>> GetCouponCodeAsync(string code);
    public Task<OperationResult> DeleteCouponCodeAsync(int id);
}
