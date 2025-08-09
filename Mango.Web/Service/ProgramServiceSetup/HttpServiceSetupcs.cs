using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service.ProgramServiceSetup;

public static class HttpServiceSetupcs
{

    public static IServiceCollection RegisterMangoCouponHttp(this IServiceCollection services, string baseCouponUrl)
    {
        // Register the HttpClient service
        services.AddHttpClient<ICouponService, CouponService>(ApiStaticUtility.HttpCouponName, client =>
        {
            client.BaseAddress = new Uri(baseCouponUrl);
        });
        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection RegisterMangoProductHttp(this IServiceCollection services, string baseCouponUrl)
    {
        // Register the HttpClient service
        services.AddHttpClient<ICouponService, CouponService>(ApiStaticUtility.HttpProductName, client =>
        {
            client.BaseAddress = new Uri(baseCouponUrl);
        });
        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection RegisterAuthHttp(this IServiceCollection services, string baseAuthUrl)
    {
        // Register the HttpClient service
        services.AddHttpClient<IAuthService, AuthService>(ApiStaticUtility.HttpAuthtName, client =>
        {
            client.BaseAddress = new Uri(baseAuthUrl);
        });

        services.AddHttpContextAccessor();
        return services;
    }
}
