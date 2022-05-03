using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface INicknameRepository
{
    public Nickname GetNickname();
}