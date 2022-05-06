using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class UserRepository:IUsersRepository
{
    private readonly IMongoCollection<User> _userTable;
    public UserRepository(string connection,string database)
    {
        var client = new MongoClient(connection);
        var db = client.GetDatabase(database);
        _userTable = db.GetCollection<User>("Users");
    }
    public List<User> UsersList()
    {
        return _userTable.Find(FilterDefinition<User>.Empty).ToList();
    }

    public User GetUserById(string id)
    {
        return _userTable.Find(x => x.Id == id).First();
    }

    public void NewUser(User user)
    {
        user.Id = ObjectId.GenerateNewId().ToString();
       _userTable.InsertOne(user);
    }
}