using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class NicknameRepository:INicknameRepository
{
    private readonly IMongoCollection<Nickname> _nicknameTable;
    public NicknameRepository(string connection)
    {
        var client = new MongoClient(connection);
        var database = client.GetDatabase("CompanyRating");
        _nicknameTable = database.GetCollection<Nickname>("Nicknames");
    }
    
    public Nickname GetNickname()
    {
        var nickname =_nicknameTable.Find(FilterDefinition<Nickname>.Empty).First();
        _nicknameTable.DeleteOne(x => x.Name == nickname.Name);
        return nickname;
    }
}