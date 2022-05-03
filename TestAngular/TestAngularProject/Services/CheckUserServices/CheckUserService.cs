using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.ServicesInterfaces;

namespace Services.CheckUserServices;

public class CheckUserService:ICheckUserService
{
    private readonly IUsersRepository _usersRepository;
    private readonly INicknameRepository _nicknameRepository;
    private readonly ILogger<CheckUserService> _logger;

    public CheckUserService(IUsersRepository usersRepository, INicknameRepository nicknameRepository, ILogger<CheckUserService> logger)
    {
        _usersRepository = usersRepository;
        _nicknameRepository = nicknameRepository;
        _logger = logger;
    }
    public void CheckUser(HttpContext context)
    {
        if (!context.Request.Cookies.ContainsKey("user_id"))
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddMonths(12);
            User user = new User();
            //Nickname name = _nicknameRepository.GetNickname();
            //author.Nickname =name.Name ;
            user.Nickname = "MishaTest";
            _usersRepository.NewUser(user);
            var newAuthor =_usersRepository.UsersList().Last().Id;
            context.Response.Cookies.Append("user_id",newAuthor.ToString(),cookieOptions);
            _logger.LogInformation($"Create new User with user id {newAuthor}");
        }
        
    }
}