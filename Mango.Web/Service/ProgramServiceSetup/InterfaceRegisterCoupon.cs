using Mango.Web.Service.IService;

namespace Mango.Web.Service.ProgramServiceSetup;

public static class InterfaceRegisterCoupon
{
    public static IServiceCollection RegisterCouponInterfaces(this IServiceCollection services)
    {
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        return services;
    }
}
