using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.ServicesInterfaces;

namespace Services.ErrorServices;

public class ErrorService:IErrorService
{
    private readonly ILogger<ErrorService> _logger;

    public ErrorService(ILogger<ErrorService> logger)
    {
        _logger = logger;
    }
    public ErrorDTO GetError(HttpContext context)
    {
        var exceptionDetails = context.Features.Get<IExceptionHandlerPathFeature>();
        ErrorDTO error = new ErrorDTO();
        error.ExceptionPath = exceptionDetails.Path;
        error.ExceptionMessage = exceptionDetails.Error.Message;
        error.StackTrace = exceptionDetails.Error.StackTrace;
        _logger.LogError($"Error {exceptionDetails.Error.Message} and stack trace {exceptionDetails.Error.StackTrace}");
        return error;
    }

    public ErrorDTO HttpStatusError(HttpContext context, int statusCode)
    {
        var statusCodeResult = context.Features.Get<IStatusCodeReExecuteFeature>();
        ErrorDTO errorDto = new ErrorDTO();
        errorDto.ExceptionPath = statusCodeResult.OriginalPath;
        errorDto.QueryString = statusCodeResult.OriginalQueryString;
        switch (statusCode)
        {
            case 404:
                errorDto.ExceptionMessage = "Sorry, resource you requested couldn't be found";
                break;
            case 500:
                errorDto.ExceptionMessage = "Enternal server error";
                break;
        }
        _logger.LogError($"Error with status code {statusCode} and path {errorDto.ExceptionPath}");
        return errorDto;
    }
}