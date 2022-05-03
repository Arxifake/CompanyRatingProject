using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class RatingRepository:IRatingsRepository
{
    private readonly IMongoCollection<Rating> _ratingTable;
    
    public RatingRepository(string connection)
    {
        var client = new MongoClient(connection);
        var database = client.GetDatabase("CompanyRating");
        _ratingTable = database.GetCollection<Rating>("Ratings");
    }

    public List<Rating> GetRatingsByCompanyId(string id)
    {
        return _ratingTable.Find(x => x.CompanyId == id).ToList();
    }

    
    public Rating GetRatingById(string id)
    {
        return _ratingTable.Find(x => x.Id == id).First();
    }

    public void AddRate(Rating rating)
    {
        rating.Id = ObjectId.GenerateNewId().ToString();
        _ratingTable.InsertOne(rating);
    }

    public void ChangeRate(Rating rate)
    {
        _ratingTable.ReplaceOne(x => x.Id == rate.Id, rate);
    }
}