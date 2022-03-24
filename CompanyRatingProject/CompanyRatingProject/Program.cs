using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services;
using Services.CompanyControllerServices;
using Services.HomeControllerServices;
using Services.ServicesInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddSingleton<ICompaniesRepository>(_ => new CompanyRepository(connection));
    builder.Services.AddSingleton<IRatingsRepository>(_ => new RatingRepository(connection));
    builder.Services.AddSingleton<IAuthorsRepository>(_ => new AuthorRepository(connection));
    builder.Services.AddSingleton<IHomeService,HomeService>();
    builder.Services.AddSingleton<ICompanyService,CompanyService>();
    builder.Services.AddSingleton<IValidateLoginService, ValidateLoginService>();
    builder.Services.AddSingleton<IDeleteCompanyService, DeleteCompanyService>();
    builder.Services.AddSingleton<IEditCompanyService, EditCompanyService>();
    builder.Services.AddSingleton<IAddCompanyService, AddCompanyService>();
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