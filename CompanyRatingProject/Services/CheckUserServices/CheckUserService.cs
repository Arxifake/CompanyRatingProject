using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NLog;
using Services.ServicesInterfaces;
using ILogger = NLog.ILogger;

namespace Services.CheckUserServices;

public class CheckUserService:ICheckUserService
{
    private readonly IAuthorsRepository _authorsRepository;
    private readonly INicknameRepository _nicknameRepository;
    private readonly ILogger<CheckUserService> _logger;

    public CheckUserService(IAuthorsRepository authorsRepository, INicknameRepository nicknameRepository, ILogger<CheckUserService> logger)
    {
        _authorsRepository = authorsRepository;
        _nicknameRepository = nicknameRepository;
        _logger = logger;
    }
    public void CheckUser(HttpContext context)
    {
        if (!context.Request.Cookies.ContainsKey("user_id"))
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddMonths(12);
            Author author = new Author();
            //Nickname name = _nicknameRepository.GetNickname();
            //author.Nickname =name.Name ;
            author.Nickname = "MishaTest";
            _authorsRepository.NewAuthor(author);
            var newAuthor =_authorsRepository.AuthorList().Last().Id;
            context.Response.Cookies.Append("user_id",newAuthor.ToString(),cookieOptions);
            _logger.LogInformation($"Create new User with user id {newAuthor}");
        }
        
    }
}