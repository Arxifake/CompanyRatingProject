using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CompanyRatingProject.Models;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeService _homeService;
    private readonly ICheckUserService _checkUserService;
    private readonly ILoginValidationService _loginValidationService;


    public HomeController(ILogger<HomeController> logger,IHomeService homeService,ILoginValidationService loginValidationService,ICheckUserService checkUserService)
    {
        _logger = logger;
        _homeService = homeService;
        _loginValidationService = loginValidationService;
        _checkUserService = checkUserService;
    }


   
    
    public IActionResult Index(string top, string current, string searchString, int? pageNumber)
    {
        var show =_homeService.ShowCompanies(top, current,searchString,pageNumber);
        _checkUserService.CheckUser(HttpContext);
        return View(show);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        _checkUserService.CheckUser(HttpContext);
        return View();
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
                TempData["Error"] = "Incorrect login or password";
                return View("Login") ;
            }
    }
}