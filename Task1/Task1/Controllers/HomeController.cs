using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task1.Interfaces;
using Task1.Models;
using Task1.Models.Interfaces;
using Task1.Models.ModelViews;

namespace Task1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICompanies _companies;
    private readonly IAuthors _authors;
    private int pageSize = 2;
    

    public HomeController(ILogger<HomeController> logger,ICompanies companies,IAuthors authors)
    {
        _logger = logger;
        _companies = companies;
        _authors = authors;
    }


   

    public IActionResult Index(string top, string current, string searchString, int? pageNumber)
    {
        ViewData["AllSort"] = "all";
        ViewData["Top10Sort"] = "top10";
        ViewData["Top25Sort"] = "top25";
        ViewData["Top50Sort"] = "top50";
        ViewData["CurrentFilter"] = searchString;
        
        
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
        if (!HttpContext.Request.Cookies.ContainsKey("user_id"))
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddMonths(12);
            Author author = new Author();
            author.Nickname = "MishaTest";
            _authors.NewAuthor(author);
            HttpContext.Response.Cookies.Append("user_id",_authors.AuthorList().Last().Id.ToString(),cookieOptions);
        }
        
        return View(Pagination<Company>.Create(companiesList,pageNumber ??1,pageSize));
    }

    public IActionResult Privacy()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}