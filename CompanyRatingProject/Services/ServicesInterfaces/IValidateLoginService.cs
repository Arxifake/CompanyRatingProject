using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface IValidateLoginService
{
    public string Validate(string username, string password, HttpContext context);
}