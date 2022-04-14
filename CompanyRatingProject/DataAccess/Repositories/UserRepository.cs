using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class UserRepository:IUsersRepository
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private IMongoCollection<User> _userTable;
    public UserRepository(string connection)
    {
        _client = new MongoClient(connection);
        _database = _client.GetDatabase("CompanyRating");
        _userTable = _database.GetCollection<User>("Users");
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