using DTO.ModelViewsObjects;

namespace Services.ServicesInterfaces;

public interface IHomeService
{
    public Page ShowCompanies(string top, string searchString, int? pageNumber);
    
}