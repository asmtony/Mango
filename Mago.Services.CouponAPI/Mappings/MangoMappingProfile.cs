using AutoMapper;
using Mago.Services.CouponAPI.Models.DbModels;
using Mago.Services.CouponAPI.Models.Dto;

namespace Mago.Services.CouponAPI.Mappings;

public class MangoMappingProfile : Profile
{
    public MangoMappingProfile()
    {
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon, CouponDto>();
    }

}
