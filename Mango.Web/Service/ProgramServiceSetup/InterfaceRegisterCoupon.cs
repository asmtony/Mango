using Mango.Web.Service.IService;

namespace Mango.Web.Service.ProgramServiceSetup
{
    public static class InterfaceRegisterCoupon
    {
        public static IServiceCollection RegisterCouponInterfaces(this IServiceCollection services)
        {
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ICouponService, CouponService>();
            //ApiStaticUtility.CouponAPIBase = "https://localhost:44329/api/v1/coupon/";
            // ApiStaticUtility.CouponAPIBase = "https://mango-coupon.azurewebsites.net/api/v1/coupon/";

            return services;
        }
    }
}
