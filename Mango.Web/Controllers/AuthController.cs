using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
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
            return View(loginRequestDto);
        }
        [HttpGet]
        public IActionResult Logout()
        {

            return View();
        }
    }
}
