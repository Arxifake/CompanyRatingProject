using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Services.ServicesInterfaces;

namespace Services.HomeServices;

public class LoginValidationService:ILoginValidationService
{
    public bool IsValid(string username,string password,HttpContext context)
    {
        if (username =="test"&& password =="test")
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("username",username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier,username));
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            context.SignInAsync(claimPrincipal);
            return true;
        }
        else
        {
            return false;
        }
    }
}