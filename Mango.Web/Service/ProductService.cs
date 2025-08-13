using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.ApiStaticUtility;


namespace Mango.Web.Service;

// Task<ResponseDto<T>> SendAsync<T, T1>(RequestDto<T1> requestDto);
public class ProductService : IProductService
{
    private readonly IBaseService _baseService;
    private readonly string _apiController;

    public ProductService(IBaseService baseService)
    {
        _apiController = "product";
        _baseService = baseService;
    }

    public async Task<ResponseDto> EditProductAsync(ProductDto productDto)
    {
        RequestDto request = BuildPostRequest(ApiTypes.POST, _apiController, productDto);
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> CreateProductAsync(ProductDto productDto)
    {
        RequestDto request = BuildPostRequest(ApiTypes.POST, _apiController, productDto);
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
        // Assuming that the URL for deleting a product is "ProductApi/{id}"
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.DELETE,
            URL = $"{_apiController}/{id}",
            AccessToken = ""
        };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> GetAllProductsAsync()
    {
        string url = _apiController;
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = url,
            AccessToken = ""
        };

        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> GetProductAsync(string ProductCode)
    {
        RequestDto request = new RequestDto { ApiType = ApiTypes.GET, URL = $"ProductApi/GetByCode", AccessToken = "" };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> GetProductByIdAsync(int id)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.GET,
            URL = $"{_apiController}/{id}",
            AccessToken = ""
        };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }

    public async Task<ResponseDto> UpdateProductAsync(ProductDto productDto/*ProductDto ProductDto*/)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.PUT,
            Data = productDto,
            URL = _apiController,
            AccessToken = ""
        };

        return await _baseService.SendAsync(request, ApiStaticUtility.HttpProductName, true);
    }
}
