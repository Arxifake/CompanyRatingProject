using DTO.ModelViewsObjects;

namespace Services.ServicesInterfaces;

public interface IHomeService
{
    public Pagination<CompanyDto> ShowCompanies(string top, string searchString, int? pageNumber);
    
}