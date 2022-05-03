using DataAccess.Models;


namespace DataAccess.Interfaces
{
    public interface ICompaniesRepository
    {
        public List<Company> CompanyList();
        public Company GetCompanyById(string id);
        public void DeleteCompany(string id);
        public void EditCompany(Company company);
        public void AddCompany(Company company);
        public void UpdateRateAvg(string id);

    }
}
