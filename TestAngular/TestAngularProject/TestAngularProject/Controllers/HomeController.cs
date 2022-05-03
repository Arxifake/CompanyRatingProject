using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace TestAngularProject.Controllers;


[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly IHomeService _homeService;
    private readonly ICheckUserService _checkUserService;
    private readonly ILoginValidationService _loginValidationService;


    public HomeController(IHomeService homeService,ILoginValidationService loginValidationService,ICheckUserService checkUserService)
    {
        _homeService = homeService;
        _loginValidationService = loginValidationService;
        _checkUserService = checkUserService;
    }


   
    [HttpGet]
    public IEnumerable<CompanyDto> Get(string top, string current, string searchString, int? pageNumber)
    {
        var show =_homeService.ShowCompanies(top, current,searchString,pageNumber);
        _checkUserService.CheckUser(HttpContext);
        return show;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        _checkUserService.CheckUser(HttpContext);
        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Validate(string username, string password)
    {
        var valid = _loginValidationService.IsValid(username, password, HttpContext);
            if (valid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                //TempData["Error"] = "Incorrect login or password";
                return Ok() ;
            }
    }
}