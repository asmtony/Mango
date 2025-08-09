using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.ApiStaticUtility;

namespace Mango.Web.Service;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;

    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto> AssignRoleAsync(RegisterRoleRequestDto registerRoleRequestDto)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = registerRoleRequestDto,
            URL = $"AuthApi/AssignRole",
            AccessToken = ""
        };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpAuthtName, false);
    }

    public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = loginRequestDto,
            URL = $"AuthApi/login",
            AccessToken = ""
        };
        return await _baseService.SendAsync(request, ApiStaticUtility.HttpAuthtName, false);
    }

    public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registerRequestDto)
    {
        RequestDto request = new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = registerRequestDto,
            URL = $"AuthApi/register",
            AccessToken = ""
        };

        return await _baseService.SendAsync(request, ApiStaticUtility.HttpAuthtName, false);
    }
}
