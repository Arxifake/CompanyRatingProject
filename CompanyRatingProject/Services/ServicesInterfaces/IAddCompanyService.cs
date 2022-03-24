using DataAccess.Models;

namespace Services.ServicesInterfaces;

public interface IAddCompanyService
{
    public void CreateCompany(Company company);
}