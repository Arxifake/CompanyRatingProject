using System;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Services.CheckUserServices;
using Services.CompanyServices;
using Services.ErrorServices;
using Services.HomeServices;
using Services.RatingServices;
using Services.ServicesInterfaces;
using Mapper = DTO.MapperDTO;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSpaStaticFiles(configuration=>configuration.RootPath="ClientApp/dist");
builder.Services.AddControllersWithViews();
string connection = "mongodb://localhost:27017/";
string db = "CompanyRating";
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
}).UseNLog();
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperDTO());
});

IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddSingleton<ICompaniesRepository>(_ => new CompanyRepository(connection,db));
    builder.Services.AddSingleton<IRatingsRepository>(_ => new RatingRepository(connection,db));
    builder.Services.AddSingleton<IUsersRepository>(_ => new UserRepository(connection,db));
    builder.Services.AddSingleton<INicknameRepository>(_ => new NicknameRepository(connection));
    builder.Services.AddSingleton<IHomeService,HomeService>();
    builder.Services.AddSingleton<ICompanyService,CompanyService>();
    builder.Services.AddSingleton<IRatingService, RatingService>();
    builder.Services.AddSingleton<ICheckUserService, CheckUserService>();
    builder.Services.AddSingleton<IErrorService, ErrorService>();
    builder.Services.AddSingleton<ILoginValidationService, LoginValidationService>();
    builder.Services.TryAddSingleton<IHttpContextAccessor,HttpContextAccessor>();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
        options.SlidingExpiration = true;
        
    });

var app = builder.Build();

//app.Environment.EnvironmentName = "Production";
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllerRoute(
    name:"default","{controller=Home}/{action=Index}");


app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
 
    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});
app.Run();
public partial class Program{}