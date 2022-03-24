using DataAccess.Models;

namespace Services.ServicesInterfaces;

public interface IEditCompanyService
{
    public Company EditCompany(int id);
    public void SaveCompany(Company company);
}