using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Services;
using WebStore.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews();
services.AddDbContext<WebStoreDB>(o 
    => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
services.AddSingleton<IEmployerData, EmployerDataMemoryService>();
services.AddSingleton<IProductData, InMemoryProductData>();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

//app.MapGet("/", () => "Hello World!");
//app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

