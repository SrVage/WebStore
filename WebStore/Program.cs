using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Services;
using WebStore.Services.InSQL;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews();
services.AddDbContext<WebStoreDB>(o 
    => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
services.AddTransient<IDbInitializer, DbInitializer>();
services.AddTransient<IEmployerData, SqlEmployerData>();
//services.AddSingleton<IProductData, InMemoryProductData>();
services.AddScoped<IProductData, SqlProductData>();
var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dataBaseInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dataBaseInitializer.InitializeAsync(RemoveBefore: false);
}

app.UseStaticFiles();
app.UseRouting();

//app.MapGet("/", () => "Hello World!");
//app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

