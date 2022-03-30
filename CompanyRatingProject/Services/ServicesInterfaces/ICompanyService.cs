using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ICompanyService
{
    public CompanyRateModelView GetCompanyRateView(int id,HttpContext context);
    public CompanyRateModelView PostCompanyRateView(int id, HttpContext context,RatingDto rating);
    public RatingDto EditRate(int id);
    public void SaveRate(RatingDto rate);
    public void CreateCompany(CompanyDto company);
    public CompanyDto GetCompany(int id);
    public void SaveCompany(CompanyDto company);
    public void DeleteCompany(int id);
    public void AddRatings();

}