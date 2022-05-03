using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;

namespace Services.ServicesInterfaces;

public interface IErrorService
{
    public ErrorDTO GetError(HttpContext context);

    public ErrorDTO HttpStatusError(HttpContext context, int statusCode);
}