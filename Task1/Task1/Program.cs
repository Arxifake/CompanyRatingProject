using Microsoft.Extensions.DependencyInjection.Extensions;
using Task1.Interfaces;
using Task1.Models.DbContent;
using Task1.Models.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddSingleton<ICompanies>(_ => new CompanyContent(connection));
    builder.Services.AddSingleton<IRaitings>(_ => new RaitingContent(connection));
    builder.Services.AddSingleton<IAuthors>(_ => new AuthorContent(connection));
    builder.Services.TryAddSingleton<IHttpContextAccessor,HttpContextAccessor>();

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

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();