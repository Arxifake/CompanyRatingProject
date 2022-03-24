using DataAccess.Interfaces;
using Services.ServicesInterfaces;

namespace Services.CompanyControllerServices;

public class DeleteCompanyService:IDeleteCompanyService
{
    private readonly ICompaniesRepository _companies;

    public DeleteCompanyService(ICompaniesRepository companyService)
    {
        _companies = companyService;
    }

    public void DeleteCompany(int id)
    {
        _companies.DeleteCompany(id);
    }
}