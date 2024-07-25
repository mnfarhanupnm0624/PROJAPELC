//using IHttpContextAccessor HttpContextAccessor;
//using IConfiguration Configuration;
//using Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;
//using IJsonHelper Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.AspNetCore.Components.Web;
//using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
//using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using DependencyInjectionSample.Interfaces;
//using DependencyInjectionSample.Services;
//using Microsoft.Identity.Web;


var builder = WebApplication.CreateBuilder(args);
//WebApplication app = builder.Build();
//builder.Services.AddControllersWithViews();
var app = builder.Build();

Console.WriteLine($"ContentRoot Path: {builder.Environment.ContentRootPath}");
Console.WriteLine($"WebRootPath: {builder.Environment.WebRootPath}");

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();
builder.Services.AddDirectoryBrowser();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginPageApelC}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
