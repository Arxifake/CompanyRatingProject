using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;
using Services.ServicesInterfaces;

namespace Services.HomeControllerServices;

public class HomeService:IHomeService
{
    private readonly ICompaniesRepository _companies;
    private readonly IAuthorsRepository _authors;
    private int pageSize = 2;
    

     public HomeService(ICompaniesRepository companies, IAuthorsRepository authors)
    {
        _companies = companies;
        _authors = authors;
    }
    public Pagination<Company> ShowCompanies(string top, string current, string searchString, int? pageNumber)
    {
        
        
        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = current;
        }
        IEnumerable<Company> companiesList = _companies.CompanyList();
        switch(top)
        {
            case "all":
                companiesList = _companies.CompanyList();
                break;
            case "top10":
                try
                {
                    companiesList = _companies.CompanyList().GetRange(0, 1);
                    break;
                }
                catch
                {
                    companiesList = _companies.CompanyList();
                    break;
                }
            case "top25":
                try
                {
                    companiesList = _companies.CompanyList().GetRange(0, 25);
                    break;
                }
                catch
                {
                    companiesList = _companies.CompanyList();
                    break;
                }
            case "top50":
                try
                {
                    companiesList = _companies.CompanyList().GetRange(0, 50);
                    break;
                }
                catch
                {
                    companiesList = _companies.CompanyList();
                    break;
                }
        }
        if (!String.IsNullOrEmpty(searchString))
        {
            companiesList = companiesList.Where(s => s.Name.Contains(searchString));
        }

        return Pagination<Company>.Create(companiesList, pageNumber ?? 1, pageSize);
         
    }

    public void CheckUser(HttpContext context)
    {
        if (!context.Request.Cookies.ContainsKey("user_id"))
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddMonths(12);
            Author author = new Author();
            author.Nickname = "MishaTest";
            _authors.NewAuthor(author);
            context.Response.Cookies.Append("user_id",_authors.AuthorList().Last().Id.ToString(),cookieOptions);
        }
        
    }
}