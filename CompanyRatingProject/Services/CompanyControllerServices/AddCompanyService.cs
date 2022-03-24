using DataAccess.Interfaces;
using DataAccess.Models;
using Services.ServicesInterfaces;

namespace Services.CompanyControllerServices;

public class AddCompanyService:IAddCompanyService
{
    private readonly ICompaniesRepository _companies;

    public AddCompanyService(ICompaniesRepository companyService)
    {
        _companies = companyService;
    }
    public void CreateCompany(Company company)
    {
     _companies.AddCompany(company);   
    }
}