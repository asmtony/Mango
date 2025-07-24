using Mango.Web.Service.ProgramServiceSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

string baseUrl = builder.Configuration["ServiceUrls:CouponAPIBaseAddress"] ?? "NotSet";
builder.Services.RegisterMangoHttp(baseUrl);

//ApiStaticUtility.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPIBaseAddress"] ?? "NotSet";
builder.Services.RegisterCouponInterfaces();
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
