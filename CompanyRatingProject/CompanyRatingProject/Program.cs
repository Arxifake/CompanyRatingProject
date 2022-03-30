using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.CompanyServices;
using Services.HomeServices;
using Services.ServicesInterfaces;
using Mapper = DTO.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Mapper());
});

IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
    builder.Services.AddSingleton<ICompaniesRepository>(_ => new CompanyRepository(connection));
    builder.Services.AddSingleton<IRatingsRepository>(_ => new RatingRepository(connection));
    builder.Services.AddSingleton<IAuthorsRepository>(_ => new AuthorRepository(connection));
    builder.Services.AddSingleton<INicknameRepository>(_ => new NicknameRepository(connection));
    builder.Services.AddSingleton<IHomeService,HomeService>();
    builder.Services.AddSingleton<ICompanyService,CompanyService>();
    builder.Services.AddSingleton<IValidateLoginService, ValidateLoginService>();
    builder.Services.TryAddSingleton<IHttpContextAccessor,HttpContextAccessor>();
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
        options.SlidingExpiration = true;
        
    });

var app = builder.Build();


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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();