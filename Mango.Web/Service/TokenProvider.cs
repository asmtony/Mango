using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(ApiStaticUtility.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool hasCookie = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApiStaticUtility.TokenCookie, out token) ?? false;

            return hasCookie is true ? token : null;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(ApiStaticUtility.TokenCookie, token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set to true if using HTTPS
                SameSite = SameSiteMode.Strict // Adjust as needed
            });
        }
    }
}
