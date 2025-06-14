using AutoMapper;
using BusinessLayer;
using BusinessLayer.Models.InputModels;
using WebMVC.Areas.Admin.Models;

namespace WebMVC;

public class AutoMapperMvcProfile : AutoMapperProfile
{
    public AutoMapperMvcProfile() : base()
    {
        CreateMap<AddGiftCardViewModel, GiftCardDto>();
        CreateMap<AddCouponCodeViewModel, CouponCodeDto>();
    }
}
