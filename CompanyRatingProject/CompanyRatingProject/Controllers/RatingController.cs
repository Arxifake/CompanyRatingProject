using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Mvc;
using Services.RatingServices;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

public class RatingController : Controller
{
    private readonly IRatingService _ratingService;
    private readonly ILogger<RatingController> _logger;

    public RatingController(IRatingService ratingService,ILogger<RatingController> logger)
    {
        _ratingService = ratingService;
        _logger = logger;
    }
    // GET
    public IActionResult EditRating(string id)
    {
        return View(_ratingService.EditRate(id));
    }

    public IActionResult SaveEditRate([Bind] RatingDto rate)
    {
        _ratingService.SaveRate(rate);
        return RedirectToAction("GetCompanyById","Company", new {id = rate.CompanyId});
    }
}