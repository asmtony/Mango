using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.ApiStaticUtility;


namespace Mango.Web.Service;

// Task<ResponseDto<T>> SendAsync<T, T1>(RequestDto<T1> requestDto);
public class ProductService : IProductService
{
    private readonly IBaseService _baseService;

    public ProductService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto> CreateProductAsync(ProductDto couponDto)
    {
        RequestDto request = BuildPostRequest(ApiTypes.POST, "CouponApi", couponDto);
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    private RequestDto BuildPostRequest(ApiTypes apiType, string url, ProductDto data)
    {
        return new RequestDto
        {
            ApiType = apiType,
            URL = url,
            Data = data,
            AccessToken = ""
        };
    }

    public async Task<ResponseDto> DeleteProductAsync(int id)
    {
        // Assuming that the URL for deleting a product is "CouponApi/{id}"
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.DELETE,
            URL = $"CouponApi/{id}",
            AccessToken = ""
        };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> GetAllProductsAsync()
    {
        string url = $"CouponApi";
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = url,
            AccessToken = ""
        };

        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> GetProductAsync(string couponCode)
    {
        RequestDto request = new RequestDto { ApiType = ApiTypes.GET, URL = $"CouponApi/GetByCode", AccessToken = "" };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> GetProductByIdAsync(int id)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = $"CouponApi/{id}",
            AccessToken = ""
        };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto/*CouponDto couponDto*/)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.PUT,
            Data = productDto,
            URL = $"CouponApi",
            AccessToken = ""
        };

        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }
}
