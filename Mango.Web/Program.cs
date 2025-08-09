using Mango.Web.Service.ProgramServiceSetup;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string baseCouponUrl = builder.Configuration["ServiceUrls:CouponAPIBaseAddress"] ?? "NotSet";
string baseProductUrl = builder.Configuration["ServiceUrls:ProductAPIBaseAddress"] ?? "NotSet";
string baseAuthUrl = builder.Configuration["ServiceUrls:AuthAPIBaseAddress"] ?? "NotSet";
builder.Services.RegisterMangoCouponHttp(baseCouponUrl);
builder.Services.RegisterMangoProductHttp(baseProductUrl);
builder.Services.RegisterAuthHttp(baseAuthUrl);

//ApiStaticUtility.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPIBaseAddress"] ?? "NotSet";
builder.Services.RegisterCouponInterfaces();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromHours(10);
            options.LoginPath = "/Auth/Login";
            // options.AccessDeniedPath = "/Auth/AccessDenied";
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
