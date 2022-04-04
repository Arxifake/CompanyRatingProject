using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface IHomeService
{
    public Pagination<CompanyDto> ShowCompanies(string top, string current, string searchString, int? pageNumber);
    
}