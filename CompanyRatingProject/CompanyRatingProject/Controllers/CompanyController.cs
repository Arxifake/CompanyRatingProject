using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

public class CompanyController : Controller
{
    private readonly ILogger<CompanyController> _logger;
    private readonly ICompanyService _companyService;
    private readonly IDeleteCompanyService _deleteCompany;
    private readonly IEditCompanyService _editCompany;
    private readonly IAddCompanyService _addCompany;
    


    public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IDeleteCompanyService deleteCompany,IEditCompanyService editCompany,
    IAddCompanyService addCompany)
    {
        _logger = logger;
        _companyService = companyService;
        _deleteCompany = deleteCompany;
        _editCompany = editCompany;
        _addCompany = addCompany;
    }

    // GET
    public IActionResult Company(int id)
    {
        return View(_companyService.GetCompanyRateView(id));
    }

    //POST
    public IActionResult CompanyRate(int id)
    {
        return View("Company",_companyService.PostCompanyRateView(id,HttpContext));
    }
    //GET
    public IActionResult DeleteCompany(int id)
    {
        return View(_companyService.GetCompanyRateView(id));
    }
    //POST
    public IActionResult SubmitDelete(int id)
    {
        _deleteCompany.DeleteCompany(id);
        return RedirectToAction("Index", "Home");
    }
    //GET
    public IActionResult EditCompany(int id)
    {
        return View(_editCompany.EditCompany(id));
    }
    //POST
    public IActionResult SaveEdit([Bind] Company company)
    {
        _editCompany.SaveCompany(company);
        return RedirectToAction("Company",new {id = company.Id});
    }
    //GET
    public IActionResult AddCompany()
    {
        return View();
    }
    //POST
    public IActionResult SaveCompany([Bind] Company company)
    {
        _addCompany.CreateCompany(company);
        return Ok();
    }
}