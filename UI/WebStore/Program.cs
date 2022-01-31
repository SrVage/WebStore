using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Services;
using WebStore.Services.Services.InCookies;
using WebStore.Interfaces.TestAPI;
using WebStore.WepAPI.Clients.Values;
using WebStore.WepAPI.Clients.Employers;
using WebStore.WepAPI.Clients.Products;
using WebStore.Services.Services.InSQL;
using WebStore.WepAPI.Clients.Orders;
using WebStore.WepAPI.Clients.Identity;

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
    //.AddEntityFrameworkStores<WebStoreDB>()
    .AddDefaultTokenProviders();
var configuration = builder.Configuration;
services.AddHttpClient("WebStoreAPIIdentity", client => client.BaseAddress = new(configuration["WebAPI"]))
   .AddTypedClient<IUserStore<User>, UsersClient>()
   .AddTypedClient<IUserRoleStore<User>, UsersClient>()
   .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
   .AddTypedClient<IUserEmailStore<User>, UsersClient>()
   .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
   .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
   .AddTypedClient<IUserClaimStore<User>, UsersClient>()
   .AddTypedClient<IUserLoginStore<User>, UsersClient>()
   .AddTypedClient<IRoleStore<Role>, RolesClient>();

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

//services.AddTransient<IEmployerData, SqlEmployerData>();
services.AddScoped<IProductData, SqlProductData>();
services.AddScoped<IOrderService, SqlOrderService>();
services.AddScoped<ICartService, InCookiesCartService>();

services.AddHttpClient("WebStoreAPI", client => client.BaseAddress = new(configuration["WebAPI"]))
.AddTypedClient<IValuesService, ValuesClient>()
.AddTypedClient<IEmployerData, EmployersClient>();
//.AddTypedClient<IProductData, ProductsClient>()
//.AddTypedClient<IOrderService, OrdersClient>();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dataBaseInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dataBaseInitializer.InitializeAsync(RemoveBefore: false);
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

