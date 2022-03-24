using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ICompanyService
{
    public CompanyRateModelView GetCompanyRateView(int id);
    public CompanyRateModelView PostCompanyRateView(int id, HttpContext context);
}