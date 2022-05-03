using Microsoft.AspNetCore.Mvc;
using Services.ServicesInterfaces;

namespace TestAngularProject.Controllers;

public class ErrorController : Controller
{
    private readonly IErrorService _error;
    public ErrorController(IErrorService error)
    {
        _error = error;
    }
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        _error.HttpStatusError(HttpContext,statusCode);
        return Ok();

    }
    [Route("Error")]
    public IActionResult Error()
    {
        return View(_error.GetError(HttpContext));
    }
}