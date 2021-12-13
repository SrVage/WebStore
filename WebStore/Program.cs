var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapDefaultControllerRoute();
app.Run();

