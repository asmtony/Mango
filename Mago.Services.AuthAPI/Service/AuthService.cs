using Mago.Services.AuthAPI.Data;
using Mago.Services.AuthAPI.Models;
using Mago.Services.AuthAPI.Models.Dto;
using Mago.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mago.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

            if (user == null)
                return false;

            //if (_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                // create role if it does not exist
                IdentityResult roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            // assign role to user
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Username.ToLower());

            if (user == null)
            {
                return new LoginResponseDto
                {
                    User = new(),
                    Token = ""
                };
            }

            bool isValidUser = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (!isValidUser)
            {
                return new LoginResponseDto
                {
                    User = new UserDto(),
                    Token = ""
                };
            }

            // user validated, generate token
            string token = _jwtTokenGenerator.GenerateToken(user);

            UserDto userDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                Name = $"{user.FirstName} {user.LastName}",
                PhoneNumber = user.PhoneNumber!
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                PhoneNumber = registrationRequestDto.PhoneNumber,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _appDbContext.Users.FirstOrDefault(u => u.UserName == registrationRequestDto.Email);
                    if (userToReturn != null)
                    {
                        new UserDto
                        {
                            Id = userToReturn.Id,
                            Email = userToReturn.Email!,
                            Name = $"{userToReturn.FirstName} {userToReturn.LastName}",
                            PhoneNumber = userToReturn.PhoneNumber!
                        };
                        return "";

                    }
                    else
                    {
                        return result.Errors.FirstOrDefault().Description;
                    }
                }
                else
                {
                    if (result.Errors.Count() > 0)
                    {
                        string errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        return errors;
                    }
                    return "Error while registering";
                }
            }
            catch (Exception ex)
            {


            }
            return "Error occured.";
        }
    }
}
