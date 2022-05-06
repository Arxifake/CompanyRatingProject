using System.Collections.Generic;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;


[ApiController]
[Route("[controller]")]
public class HomeController : Controller
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
    public IActionResult Index()
    {
        return View();
    }
    [Route("Get")]
    public Pagination<CompanyDto> Get(string? top, string? searchString, int? pageNumber)
    {
        var show =_homeService.ShowCompanies(top,searchString,pageNumber);
        _checkUserService.CheckUser(HttpContext);
        return show;
    }

    [HttpPost("login")]
    public IActionResult Validate([FromBody]UserForLogin user)
    {
        var valid = _loginValidationService.IsValid(user.Login, user.Password, HttpContext);
            if (valid)
            {
                return Ok(new AuthResponse{IsAuth = true});
            }
            else
            {
                return Unauthorized("Incorrect login or password");
            }
    }
}