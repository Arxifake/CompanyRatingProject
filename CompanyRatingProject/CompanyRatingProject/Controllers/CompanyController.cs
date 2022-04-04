using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyService _companyService;
    private readonly ICheckUserService _checkUserService;


    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService,ICheckUserService checkUserService)
    {
        _logger = logger;
        _companyService = companyService;
        _checkUserService = checkUserService;
    }

    // GET
    public IActionResult GetCompanyById(int id)
    {

        try
        {
            _checkUserService.CheckUser(HttpContext);
            var companyRateView = _companyService.GetCompanyRateView(id, HttpContext);
            return View(companyRateView);
        }
        catch (NullReferenceException e)
        {
            return RedirectToAction("Index", "Home");
        }
    }

    //POST
    public IActionResult AddCompanyRate([Bind]RatingDto rating)
    {
        _companyService.PostCompanyRateView(HttpContext, rating);
        return RedirectToAction("GetCompanyById", new {id = rating.CompanyId});

        }
    //GET
    [Authorize]
    public IActionResult DeleteCompany(int id)
    {
        return View(_companyService.GetCompany(id));
    }
    //POST
    [Authorize]
    public IActionResult SubmitDelete(int id)
    {
        _companyService.DeleteCompany(id,HttpContext);
        return RedirectToAction("Index", "Home");
    }
    //GET
    [Authorize]
    public IActionResult EditCompany(int id)
    {
        return View(_companyService.GetCompany(id));
    }
    //POST
    [Authorize]
    public IActionResult SaveEdit([Bind] CompanyDto company)
    {
        _companyService.SaveCompany(company,HttpContext);
        return RedirectToAction("GetCompanyById",new {id = company.Id});

    }
    //GET
    [Authorize]
    public IActionResult AddCompany()
    {
        return View();
    }
    //POST
    [Authorize]
    public IActionResult SaveCompany([Bind] CompanyDto company)
    {
        _companyService.CreateCompany(company, HttpContext);
        return RedirectToAction("Index","Home");
    }
}