using DataAccess.Models;

namespace DataAccess.Interfaces;

public interface IUsersRepository
{
    public List<User> UsersList();
    public User GetUserById(string id);
    public void NewUser(User author);
}