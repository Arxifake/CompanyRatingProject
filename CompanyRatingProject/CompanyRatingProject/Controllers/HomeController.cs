using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CompanyRatingProject.Models;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeService _homeService;
    private readonly IValidateLoginService _validate;


    public HomeController(ILogger<HomeController> logger,IHomeService homeService,IValidateLoginService validate)
    {
        _logger = logger;
        _homeService = homeService;
        _validate = validate;
    }


   
    
    public IActionResult Index(string top, string current, string searchString, int? pageNumber)
    {
        ViewData["AllSort"] = "all";
        ViewData["Top10Sort"] = "top10";
        ViewData["Top25Sort"] = "top25";
        ViewData["Top50Sort"] = "top50";
        ViewData["CurrentFilter"] = searchString;
        ViewData["topSort"] = top;

        var show =_homeService.ShowCompanies(top, current,searchString,pageNumber);
        _homeService.CheckUser(HttpContext);
        
        return View(show);
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
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
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public IActionResult AddManyCompanies()
    {
        _homeService.AddCompanies();
        return RedirectToAction("Index");
    }
}