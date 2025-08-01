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

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDto?> SendAsyncCoupon(RequestDto requestDto)
    {
        string httpName = "MangoCoupon";
        return await SendAsync(requestDto, httpName);
    }

    public async Task<ResponseDto?> SendAsyncAuth(RequestDto requestDto)
    {
        string httpName = "MangoAuth";
        return await SendAsync(requestDto, httpName);
    }

    private async Task<ResponseDto?> SendAsync(RequestDto requestDto, string httpName)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient(httpName);

            HttpRequestMessage message = new(GetMessageType(requestDto.ApiType), requestDto.URL);
            message.Headers.Add("Accept", "application/json");

            if (requestDto.Data != null)
            {
                message.Content = new StringContent(
                    JsonSerializer.Serialize(requestDto.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponse = null;

            apiResponse = await client.SendAsync(message);
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
