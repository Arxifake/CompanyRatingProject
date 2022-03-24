using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface IHomeService
{
    public Pagination<Company> ShowCompanies(string top, string current, string searchString, int? pageNumber);
    public void CheckUser(HttpContext context);
}