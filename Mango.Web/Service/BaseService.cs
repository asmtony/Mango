using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Mango.Web.Service;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProvider _tokenProvider;

    public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProvider = tokenProvider;
    }

    //public async Task<ResponseDto?> SendAsyncCoupon(RequestDto requestDto, bool withBearer = true)
    //{
    //    string httpName = ApiStaticUtility.HttpCouponName;
    //    return await SendAsync(requestDto, httpName, withBearer);
    //}

    //public async Task<ResponseDto?> SendAsyncProduct(RequestDto requestDto, bool withBearer = true)
    //{
    //    string httpName = ApiStaticUtility.HttpProductName;
    //    return await SendAsync(requestDto, httpName, withBearer);
    //}

    //public async Task<ResponseDto?> SendAsyncAuth(RequestDto requestDto)
    //{
    //    string httpName = ApiStaticUtility.HttpAuthtName;
    //    return await SendAsync(requestDto, httpName);
    //}

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto, string httpName, bool withBearer)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient(httpName);

            HttpRequestMessage message = new(GetMessageType(requestDto.ApiType), requestDto.URL);
            message.Headers.Add("Accept", "application/json");

            if (withBearer)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(
                    JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = await client.SendAsync(message);
            return await GetResponseMessge(apiResponse);
        }
        catch (Exception ex)
        {
            var logerror = ex.Message;
            var dto = new ResponseDto
            {
                IsSuccess = false,
                Message = "Error while calling API"
            };
            return dto;
        }
    }

    private HttpMethod GetMessageType(ApiStaticUtility.ApiTypes apiTypes)
    {
        switch (apiTypes)
        {
            case ApiStaticUtility.ApiTypes.POST:
                return HttpMethod.Post;
            case ApiStaticUtility.ApiTypes.PUT:
                return HttpMethod.Put;
            case ApiStaticUtility.ApiTypes.DELETE:
                return HttpMethod.Delete;
            default:
                return HttpMethod.Get;
        }
    }

    private async Task<ResponseDto> GetResponseMessge(HttpResponseMessage apiResponse)
    {
        switch (apiResponse.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return new() { IsSuccess = false, Message = "Resource not found" };
            case HttpStatusCode.Forbidden:
                return new() { IsSuccess = false, Message = "Access Denied" };
            case HttpStatusCode.Unauthorized:
                return new() { IsSuccess = false, Message = "Unauthorised" };
            case HttpStatusCode.InternalServerError:
                return new() { IsSuccess = false, Message = "Internal Server Error" };
            default:
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonSerializer.Deserialize<ResponseDto>(apiContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponseDto ?? new();
        }
    }

}
