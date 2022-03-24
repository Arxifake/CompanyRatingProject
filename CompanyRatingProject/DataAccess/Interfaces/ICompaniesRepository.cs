using DataAccess.Models;


namespace DataAccess.Interfaces
{
    public interface ICompaniesRepository
    {
        public List<Company> CompanyList();
        public Company GetCompanyById(int id);
        public void DeleteCompany(int id);
        public void EditCompany(Company company);
        public void AddCompany(Company company);

    }
}
