using Mago.Services.AuthAPI.Models.Dto;
using Mago.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Mago.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequest)
        {
            ResponseDto<object> responseDto = new();
            var errorMessage = await _authService.Register(registrationRequest);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                responseDto = new()
                {
                    IsSuccess = false,
                    Message = errorMessage ?? "An error occurred during registration.",
                };
                return BadRequest(responseDto);
            }

            return Ok(responseDto);
        }

        [HttpPost("login")]
        public async Task<LoginResponseDto> Login([FromBody] LoginRequestDto request)
        {
            var loginResponse = await _authService.Login(request);
            // Logic for user login
            if (loginResponse.User == null || loginResponse.User.Id == null || loginResponse.User.Id.Length == 0)
            {
                var responseDto = new ResponseDto<object>
                {
                    IsSuccess = false,
                    Message = "Invalid username or password."
                };

                return loginResponse;
            }
            return loginResponse;
        }

        [HttpPost("AssignRole")]
        public async Task<LoginResponseDto> AssignRole([FromBody] RegisterRoleRequestDto registerRoleRequestDto)
        {
            bool assignRole = await _authService.AssignRole(registerRoleRequestDto.Email, registerRoleRequestDto.RoleName.ToUpper());


        }
    }
}
