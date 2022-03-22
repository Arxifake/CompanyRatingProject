using Microsoft.AspNetCore.Mvc;
using Task1.Interfaces;
using Task1.Models.Interfaces;
using Task1.Models.ModelViews;

namespace Task1.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanies _companies;
    private readonly IRaitings _raitings;
    private readonly IAuthors _authors;


    public CompanyController(ILogger<CompanyController> logger, ICompanies companies, IRaitings raitings,IAuthors authors)
    {
        _logger = logger;
        _companies = companies;
        _raitings = raitings;
        _authors = authors;
    }

    // GET
    public IActionResult Company(int id,CompanyRateModelView companyRate)
    {
        companyRate.Company = _companies.getCompany(id);
        companyRate.Company.Ratings = _raitings.getCompanyRaitings(id);
        _authors.AuthorList();
        foreach (var rating in companyRate.Company.Ratings)
        {
            rating.Author = _authors.getAuthor(rating.AuthorId);
        }
        companyRate.Rating = new Rating();
        return View(companyRate);
    }

    //POST
    public IActionResult CompanyRate(int id,CompanyRateModelView companyRate )
    {
        companyRate.Company = _companies.getCompany(id);
        companyRate.Company.Ratings = _raitings.getCompanyRaitings(id);
        _authors.AuthorList();
        foreach (var rating in companyRate.Company.Ratings)
        {
            rating.Author = _authors.getAuthor(rating.AuthorId);
        }
        companyRate.Rating.CompanyId = companyRate.Company.Id;
        // ReSharper disable once PossibleLossOfFraction
        companyRate.Rating.Total = (companyRate.Rating.Grade1 + companyRate.Rating.Grade2 + companyRate.Rating.Grade3 +
                                    companyRate.Rating.Grade4 + companyRate.Rating.Grade5)/5;
        companyRate.Rating.AuthorId = Int32.Parse(HttpContext.Request.Cookies["user_id"]);
        companyRate.Rating.DateTime = DateTime.Now;
        companyRate.Rating.Text = "";
        _raitings.AddRate(companyRate.Rating);
        return View("Company",companyRate);
    }

    public IActionResult Exeption(Exception e)
    {
        return View(e);
    }
}