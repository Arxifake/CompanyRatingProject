using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ILoginValidationService
{
    public bool IsValid(string username, string password, HttpContext context);
}