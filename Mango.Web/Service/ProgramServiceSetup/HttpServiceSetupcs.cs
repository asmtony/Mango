using Mango.Web.Service.IService;

namespace Mango.Web.Service.ProgramServiceSetup
{
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
            //services.AddHttpClient();
            //services.AddHttpClient<ICouponService, CouponService>();

            return services;
        }
    }

}
