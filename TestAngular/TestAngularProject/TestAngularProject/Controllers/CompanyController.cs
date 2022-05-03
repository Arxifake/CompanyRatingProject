using CompanyRatingProject.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace TestAngularProject.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : Controller
{
    private readonly ICompanyService _companyService;
    private readonly ICheckUserService _checkUserService;


    public CompanyController( ICompanyService companyService,ICheckUserService checkUserService)
    {
        _companyService = companyService;
        _checkUserService = checkUserService;
    }

    [HttpGet]
    public IActionResult GetCompanyById(string id)
    {

        try
        {
            _checkUserService.CheckUser(HttpContext);
            var companyRateView = _companyService.GetCompanyRateView(id, HttpContext);
            return Ok();
        }
        catch (CompanyNotFoundException e)
        {
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    public IActionResult AddCompanyRate([Bind]RatingDto rating)
    {
        _companyService.PostCompanyRateView(HttpContext, rating);
        return RedirectToAction("GetCompanyById", new {id = rating.CompanyId});

        }
    [HttpGet]
    [Authorize]
    public IActionResult DeleteCompany(string id)
    {
        return Ok();
    }
    [HttpPost]
    [Authorize]
    public IActionResult SubmitDelete(string id)
    {
        _companyService.DeleteCompany(id,HttpContext);
        return RedirectToAction("Index", "Home");
    }
    [HttpGet]
    [Authorize]
    public IActionResult EditCompany(string id)
    {
        return Ok();
    }
    [HttpPost]
    [Authorize]
    public IActionResult SaveEdit([Bind] CompanyDto company)
    {
        _companyService.SaveCompany(company,HttpContext);
        return RedirectToAction("GetCompanyById",new {id = company.Id});

    }
    [HttpGet]
    [Authorize]
    public IActionResult AddCompany()
    {
        return Ok();
    }
    [HttpPost]
    [Authorize]
    public IActionResult SaveCompany([Bind] CompanyDto company)
    {
        _companyService.CreateCompany(company, HttpContext);
        return RedirectToAction("Index","Home");
    }
}