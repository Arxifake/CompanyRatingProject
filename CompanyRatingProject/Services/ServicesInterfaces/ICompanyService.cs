using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ICompanyService
{
    public CompanyRateModelView GetCompanyRateView(int id,HttpContext context);
    public CompanyRateModelView PostCompanyRateView(HttpContext context,RatingDto rating);
    public void CreateCompany(CompanyDto company, HttpContext context);
    public CompanyDto GetCompany(int id);
    public void SaveCompany(CompanyDto company,HttpContext context);
    public void DeleteCompany(int id, HttpContext context);


}