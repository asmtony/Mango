using Mago.Services.CouponAPI.Models.Dto;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using static Mango.Web.Utility.ApiStaticUtility;


namespace Mango.Web.Service;

// Task<ResponseDto<T>> SendAsync<T, T1>(RequestDto<T1> requestDto);
public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto> CreateCouponAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = couponDto,
            URL = $"CouponApi",
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> DeleteCouponAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiTypes.DELETE,
            URL = $"CouponApi/{id}",
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> GetAllCouponsAsync()
    {
        string url = $"CouponApi";
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = url,
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> GetCouponAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = $"CouponApi/GetByCode",
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> GetCouponByIdAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = $"CouponApi/{id}",
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> UpdateCouponAsync(CouponDto couponDto/*CouponDto couponDto*/)
    {
        return await _baseService.SendAsync(new RequestDto
        {
            ApiType = ApiTypes.PUT,
            Data = couponDto,
            URL = $"CouponApi",
            AccessToken = ""
        });
    }

    //public async Task<ResponseDto<T>> CreateCouponAsync<T, T1>(T1 couponDto/*CouponDto couponDto*/)
    //{
    //    return await _baseService.SendAsync<T, T1>(new RequestDto<T1>
    //    {
    //        ApiType = ApiTypes.POST,
    //        Data = couponDto,
    //        URL = $"{CouponAPIBase}Coupon",
    //        AccessToken = ""
    //    });
    //}

    //public async Task<ResponseDto<T>> DeleteCouponAsync<T, T1>(int id)
    //{
    //    return await _baseService.SendAsync<T, T1>(new RequestDto<T1>
    //    {
    //        ApiType = ApiTypes.DELETE,
    //        URL = $"{CouponAPIBase}Coupon/{id}",
    //        AccessToken = ""
    //    });
    //}

    //public async Task<ResponseDto<T>> GetAllCouponsAsync<T,T1>()
    //{
    //    return await _baseService.SendAsync<T, T1>(new RequestDto<T1>
    //    {
    //        ApiType = ApiTypes.GET,
    //        URL = $"{CouponAPIBase}Coupon",
    //        AccessToken = ""
    //    });
    //}

    //public async Task<ResponseDto<T>> GetCouponAsync<T, T1>(string couponCode)
    //{
    //    return await _baseService.SendAsync<T, T1>(new RequestDto<T1>
    //    {
    //        ApiType = ApiTypes.GET,
    //        URL = $"{CouponAPIBase}Coupon/GetByCode{couponCode}",
    //        AccessToken = ""
    //    });
    //}

    //public async Task<ResponseDto<T>> GetCouponByIdAsync<T, T1>(int id)
    //{
    //    return await _baseService.SendAsync<T, T1>(new RequestDto<T1>
    //    {
    //        ApiType = ApiTypes.GET,
    //        URL = $"{CouponAPIBase}Coupon/{id}",
    //        AccessToken = ""
    //    });
    //}

    //public async Task<ResponseDto<T>> UpdateCouponAsync<T, T1>(T1 couponDto/*CouponDto couponDto*/)
    //{
    //    return await _baseService.SendAsync<T, T1>(new RequestDto<T1>
    //    {
    //        ApiType = ApiTypes.PUT,
    //        Data = couponDto,
    //        URL = $"{CouponAPIBase}Coupon",
    //        AccessToken = ""
    //    });
    //}
}
