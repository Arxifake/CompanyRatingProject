using DataAccess.Interfaces;
using DataAccess.Models;
using Services.ServicesInterfaces;

namespace Services.CompanyControllerServices;

public class EditCompanyService:IEditCompanyService
{
    private readonly ICompaniesRepository _companies;

    public EditCompanyService(ICompaniesRepository companyService)
    {
        _companies = companyService;
    }

    public Company EditCompany(int id)
    {
       return _companies.GetCompanyById(id);
    }

    public void SaveCompany(Company company)
    {
        _companies.EditCompany(company);
    }
}