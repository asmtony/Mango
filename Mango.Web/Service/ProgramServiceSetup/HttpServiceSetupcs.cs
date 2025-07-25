using Mango.Web.Service.IService;

namespace Mango.Web.Service.ProgramServiceSetup;

public static class HttpServiceSetupcs
{

    public static IServiceCollection RegisterMangoHttp(this IServiceCollection services, string baseCouponUrl)
    {
        // Register the HttpClient service
        services.AddHttpClient<ICouponService, CouponService>("MangoCoupon", client =>
        {
            client.BaseAddress = new Uri(baseCouponUrl);
        });
        services.AddHttpContextAccessor();
        return services;
    }

    public static IServiceCollection RegisterAuthHttp(this IServiceCollection services, string baseAuthUrl)
    {
        // Register the HttpClient service
        services.AddHttpClient<IAuthService, AuthService>("MangoAuth", client =>
        {
            client.BaseAddress = new Uri(baseAuthUrl);
        });

        services.AddHttpContextAccessor();
        return services;
    }
}
