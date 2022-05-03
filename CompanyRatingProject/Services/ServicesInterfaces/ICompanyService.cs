using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ICompanyService
{
    public CompanyRateModelView GetCompanyRateView(string id,HttpContext context);
    public void PostCompanyRateView(HttpContext context,RateDto rating);
    public void CreateCompany(CompanyCreateDto company, HttpContext context);
    public CompanyMainDto GetCompany(string id);
    public void SaveCompany(CompanyMainDto company,HttpContext context);
    public void DeleteCompany(string id, HttpContext context);


}