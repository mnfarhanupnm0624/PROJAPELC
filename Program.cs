//using System;
using System.IO;
using static LinqToDB.Common.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using static Microsoft.Extensions.Configuration.IConfigurationBuilder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using System.Security.Cryptography.X509Certificates;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Grpc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;
using System.Configuration;
//using WebApplication1.Services;

//var builder = new ConfigurationBuilder()
//           .SetBasePath(Directory.GetCurrentDirectory())
//           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);  // untuk appsettings.json localhost publish to IIS

//var builder = new ConfigurationBuilder().WebApplication.CreateBuilder(args)
//           .SetBasePath(Directory.GetCurrentDirectory())
//           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);  // untuk appsettings.json localhost publish to IIS

var builder = WebApplication.CreateBuilder(args); // untuk launchsettings.json localhost tanpa publish or appsettings.json localhost publish to IIS
// Configure the order of configuration sources

var connectionString = builder.Configuration.GetConnectionString("UPNMDBMYSQL");
var settingsAppName = builder.Configuration["APELC:Application"];

Console.WriteLine("connectionString=" + connectionString);
Console.WriteLine("settingsAppName=" + settingsAppName);

//IConfigurationRoot configuration;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ConfigureHttpsDefaults(httpsOptions =>
//    {
//        var certPath = Path.Combine(builder.Environment.ContentRootPath, "cert.pem");
//        var keyPath = Path.Combine(builder.Environment.ContentRootPath, "key.pem");

//        httpsOptions.ServerCertificate = X509Certificate2.CreateFromPemFile(certPath,
//                                         keyPath);
//    });
//});



//IConfigurationRoot configuration = builder.Build();

//var greeting = configuration.GetValue(typeof(String), "AppSettings:Greeting").ToString();

//var myConnectString = builder.Configuration.GetConnectionString("MyConnectionString");

//var myConnectString = builder.Configuration.GetConnectionString("DatabaseMySQL8:ConnectionStrings:Default");
//var myConnectString = builder.Configuration.GetConnectionString("DefaultConnection");
//var settingsAppName = builder.Configuration["Settings:AppName"];
//var settingsContactEmail = builder.Configuration["Settings:ContactEmail"];
//var settingsAddress = builder.Configuration["Settings:Address"];
//var theme = builder.Configuration["Theme"];

//Console.WriteLine("myConnectString=" + myConnectString);
//Console.WriteLine("settingsAppName=" + settingsAppName);
//Console.WriteLine("settingsContactEmail=" + settingsContactEmail);
//Console.WriteLine("settingsAddress=" + settingsAddress);
//Console.WriteLine("theme=" + theme);
//Console.WriteLine(greeting);
//Console.WriteLine(myConnectString);
//Console.Read();

//var builder = new ConfigurationBuilder()
//           .SetBasePath(Directory.GetCurrentDirectory())
//           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);



//var greeting = configuration.GetValue(typeof(String), "APELC:applicationUrl").ToString();



//Console.WriteLine(greeting);
//Console.WriteLine(myConnectString);
//Console.Read();

//// Add services to the container.
//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.CheckConsentNeeded = context => false;
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//});

builder.Services.AddHttpContextAccessor();
// Step 1 to use session  
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services
//    .AddConfig(builder.Configuration)
//    .AddMyDependencyGroup();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<MySqlConnection>(_ => new MySqlConnection(connectionString));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddSession();



if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //The default HSTS value is 30 days.You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginPageApelC}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
