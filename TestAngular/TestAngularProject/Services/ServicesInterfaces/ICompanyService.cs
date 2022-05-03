using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ICompanyService
{
    public CompanyRateModelView GetCompanyRateView(string id,HttpContext context);
    public CompanyRateModelView PostCompanyRateView(HttpContext context,RatingDto rating);
    public void CreateCompany(CompanyDto company, HttpContext context);
    public CompanyMainDto GetCompany(string id);
    public void SaveCompany(CompanyDto company,HttpContext context);
    public void DeleteCompany(string id, HttpContext context);


}