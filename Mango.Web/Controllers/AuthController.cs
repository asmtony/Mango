using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto
            {
                Username = string.Empty,
                Password = string.Empty
            };
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {

            if (ModelState.IsValid)
            {
                ResponseDto response = _authService.LoginAsync(loginRequestDto).Result;
                if (response != null && response.IsSuccess && response.Result != null)
                {
                    //string token = response.Token;
                    string userString = response.Result.ToString();
                    LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(response.Result.ToString());

                    await SignInUser(loginResponse);

                    // Assuming the response contains a token or user information
                    TempData["success"] = "Login successful!";
                    _tokenProvider.SetToken(loginResponse.Token);
                    return RedirectToAction("Index", "Home");
                    //RedirectToAction("CouponIndex", nameof(CouponController));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your credentials.");
                    response = response ?? new ResponseDto();
                    string errorMessage = response.Message == null || string.IsNullOrWhiteSpace(response.Message) ? "An error occurred during login." : response.Message;
                    TempData["error"] = errorMessage;
                }
            }
            else
            {
                TempData["error"] = "Invalid login attempt. Please check your credentials.";
            }

            return View(loginRequestDto);
        }

        private async Task SignInUser(LoginResponseDto loginResponse)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(loginResponse.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwtToken.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            //, 
            //    new AuthenticationProperties
            //    {
            //        IsPersistent = true,
            //        ExpiresUtc = DateTime.UtcNow.AddMinutes(30) // Set the expiration time as needed
            //    });


        }

        [HttpGet]
        public IActionResult Register()
        {
            RegistrationRequestDto loginRequestDto = new RegistrationRequestDto
            {
                Email = string.Empty,
                Password = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
            };

            var rolesList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = ApiStaticUtility.RoleAdmin },
                new SelectListItem { Text = "Customer", Value = ApiStaticUtility.RoleCustomer }
            };
            ViewBag.RolesList = rolesList;
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationRequestDto)
        {

            ResponseDto result = await _authService.RegisterAsync(registrationRequestDto);

            if (result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(registrationRequestDto.Role))
                {
                    registrationRequestDto.Role = ApiStaticUtility.RoleCustomer;
                }
                ResponseDto assignRole = await _authService.AssignRoleAsync(new RegisterRoleRequestDto
                {
                    Email = registrationRequestDto.Email,
                    Password = registrationRequestDto.Password,
                    RoleName = registrationRequestDto.Role
                });

                if (assignRole != null && assignRole.IsSuccess)
                {
                    TempData["success"] = $"Hurray! Registration successful for {registrationRequestDto.Email} :)";
                    RedirectToAction(nameof(Login));
                }
                else
                {
                    assignRole = assignRole ?? new ResponseDto();
                    TempData["error"] = assignRole.Message ?? "An error occurred while assigning the role. :( ";
                }
            }
            else
            {
                result = result ?? new ResponseDto();
                string errorMessage = result.Message == null || string.IsNullOrWhiteSpace(result.Message) ? "An error occurred during registration." : result.Message;
                TempData["error"] = errorMessage;
            }
            return View(registrationRequestDto);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _tokenProvider.ClearToken();

            return RedirectToAction("Index", "Home");
        }
    }
}
