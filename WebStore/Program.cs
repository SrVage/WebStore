﻿var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddControllersWithViews();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

