using CompanyRatingProject.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

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

    [Route("Get/{id}")]
    [HttpGet]
    public CompanyRateModelView GetCompanyById(string id)
    {

        try
        {
            _checkUserService.CheckUser(HttpContext);
            var companyRateView = _companyService.GetCompanyRateView(id, HttpContext);
            return companyRateView;
        }
        catch (CompanyNotFoundException e)
        {
            return null;
        }
    }
    
    [HttpPost]
    [Route("Rate")]
    public IActionResult AddCompanyRate([FromBody]RateDto rating)
    {
        _companyService.PostCompanyRateView(HttpContext, rating);
        return Ok();

    }
    [Route("GetInfo/{id}")]
    [HttpGet]
    [Authorize]
    public CompanyMainDto GetCompanyInfo(string id)
    {
        return _companyService.GetCompany(id);
    }
    [HttpPost]
    [Authorize]
    [Route("DeleteCompany")]
    public IActionResult SubmitDelete([FromBody]CompanyIdDto id)
    {
        _companyService.DeleteCompany(id.Id,HttpContext);
        return Ok();
    }
    
    [HttpPost]
    [Authorize]
    [Route("EditChange")]
    public IActionResult SaveEdit([FromBody] CompanyMainDto company)
    {
        _companyService.SaveCompany(company,HttpContext);
        return Ok();

    }
    
    [HttpPost]
    [Authorize]
    [Route("Create")]
    public IActionResult SaveCompany([FromBody] CompanyCreateDto company)
    {
        _companyService.CreateCompany(company, HttpContext);
        return Ok();
    }
}