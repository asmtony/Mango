using Mago.Services.AuthAPI.Models.Dto;

namespace Mago.Services.AuthAPI.Service.IService;

public interface IAuthService
{
    public Task<string> Register(RegistrationRequestDto registrationRequestDto);
    public Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    public Task<bool> AssignRole(string email, string roleName);
}
