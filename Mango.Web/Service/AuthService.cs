using Mango.Web.Models;
using Mango.Web.Service.IService;
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
        return await _baseService.SendAsyncAuth(new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = registerRoleRequestDto,
            URL = $"AuthApi/AssignRole",
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
    {
        return await _baseService.SendAsyncAuth(new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = loginRequestDto,
            URL = $"AuthApi/login",
            AccessToken = ""
        });
    }

    public async Task<ResponseDto> RegisterAsync(RegistrationRequestDto registerRequestDto)
    {
        return await _baseService.SendAsyncAuth(new RequestDto
        {
            ApiType = ApiTypes.POST,
            Data = registerRequestDto,
            URL = $"AuthApi/register",
            AccessToken = ""
        });
    }
}
