using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace TestAngularProject.Controllers;

public class RatingController : Controller
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }
    [HttpGet]
    public IActionResult EditRating(string id)
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult SaveEditRate([Bind] RatingDto rate)
    {
        _ratingService.SaveRate(rate);
        return RedirectToAction("GetCompanyById","Company", new {id = rate.CompanyId});
    }
}