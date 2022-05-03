using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace CompanyRatingProject.Controllers;

[ApiController]
[Route("[controller]")]
public class RatingController : Controller
{
    private readonly IRatingService _ratingService;

    public RatingController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }
    [HttpGet]
    [Route("EditRating/{id}")]
    public RatingDto EditRating(string id)
    {
        return _ratingService.EditRate(id);
    }
    
    [HttpPost]
    [Route("EditSubmit")]
    public IActionResult SaveEditRate([Bind] RatingDto rate)
    {
        _ratingService.SaveRate(rate);
        return RedirectToAction("GetCompanyById","Company", new {id = rate.CompanyId});
    }
}