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
    private readonly IValidateLoginService _validate;


    public HomeController(ILogger<HomeController> logger,IHomeService homeService,IValidateLoginService validate,ICheckUserService checkUserService)
    {
        _logger = logger;
        _homeService = homeService;
        _validate = validate;
        _checkUserService = checkUserService;
    }


   
    
    public IActionResult Index(string top, string current, string searchString, int? pageNumber)
    {
        ViewData[Constants.TopRatings.AllSort] = Constants.TopRatings.All;
        ViewData[Constants.TopRatings.Top10Sort] = Constants.TopRatings.Top10;
        ViewData[Constants.TopRatings.Top25Sort] = Constants.TopRatings.Top25;
        ViewData[Constants.TopRatings.Top50Sort] = Constants.TopRatings.Top50;
        ViewData[Constants.TopRatings.CurrentFilter] = searchString;
        ViewData[Constants.TopRatings.TopSort] = top;
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
        var valid = _validate.Validate(username, password, HttpContext);
            if (valid =="Login Complete")
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = valid;
                return View("Login") ;
            }
    }
}