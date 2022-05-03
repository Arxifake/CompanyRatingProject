using System.ComponentModel.DataAnnotations;

namespace DTO.ModelViewsObjects;

public class UserForLogin
{
    public string Login { get; set; }
    public string Password { get; set; }
}