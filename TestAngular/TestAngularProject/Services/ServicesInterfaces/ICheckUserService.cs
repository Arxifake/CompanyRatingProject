using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface ICheckUserService
{
    public void CheckUser(HttpContext context);
}