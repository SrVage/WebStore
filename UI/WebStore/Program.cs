using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Services;
using Microsoft.AspNetCore.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Services;
using WebStore.Services.Services.InCookies;
using WebStore.Services.Services.InSQL;
using WebStore.Interfaces.TestAPI;
using WebStore.WepAPI.Clients.Values;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews();
var databaseType = builder.Configuration["Database"];
switch (databaseType)
{
    case "SqlServer":
        services.AddDbContext<WebStoreDB>(o
    => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
        break;
    case "Sqlite":
        services.AddDbContext<WebStoreDB>(o
    => o.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"), o=> o.MigrationsAssembly("WebStore.DAL.Sqlite")));
        break;
}
services.AddTransient<IDbInitializer, DbInitializer>();
services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WebStoreDB>()
    .AddDefaultTokenProviders();
services.Configure<IdentityOptions>(opt =>
{
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif
    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";
    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});
services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = "WebStore.MK";
    opt.Cookie.HttpOnly = true;

    opt.ExpireTimeSpan = TimeSpan.FromDays(10);

    opt.LoginPath = "/Account/Login";
    opt.LogoutPath = "/Account/Logout";
    opt.AccessDeniedPath = "/Account/AccessDenied";

    opt.SlidingExpiration = true;
});

services.AddTransient<IEmployerData, SqlEmployerData>();

//services.AddSingleton<IProductData, InMemoryProductData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IOrderService, SqlOrderService>();
services.AddScoped<ICartService, InCookiesCartService>();
var configuration = builder.Configuration;
services.AddHttpClient<IValuesService, ValuesClient>(client => client.BaseAddress=new(configuration["WebAPI"]));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dataBaseInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dataBaseInitializer.InitializeAsync(RemoveBefore: false);
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});
//app.MapGet("/", () => "Hello World!");
//app.MapDefaultControllerRoute();
app.Run();

