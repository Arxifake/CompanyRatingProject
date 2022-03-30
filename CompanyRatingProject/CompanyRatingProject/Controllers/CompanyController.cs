using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyService _companyService;



    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
    {
        _logger = logger;
        _companyService = companyService;
    }

    // GET
    public IActionResult Company(int id)
    {
        var x =_companyService.GetCompanyRateView(id,HttpContext);
        if (x.Company.Name!=null)
        {
            return View(x);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
        
    }

    //POST
    public IActionResult CompanyRate(int id,[Bind]RatingDto rating)
    {
        _companyService.PostCompanyRateView(id, HttpContext, rating);
        return RedirectToAction("Company", new {id = id});
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
        _companyService.DeleteCompany(id);
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
        _companyService.SaveCompany(company);
        return RedirectToAction("Company",new {id = company.Id});
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
        _companyService.CreateCompany(company);
        return RedirectToAction("Index","Home");
    }

    public IActionResult EditRating(int id)
    {
        return View(_companyService.EditRate(id));
    }

    public IActionResult SaveEditRate([Bind] RatingDto rate)
    {
        _companyService.SaveRate(rate);
        return RedirectToAction("Company", new {id = rate.CompanyId});
    }

    public IActionResult AddRatings()
    {
        _companyService.AddRatings();
        return RedirectToAction("Index", "Home");
    }
}