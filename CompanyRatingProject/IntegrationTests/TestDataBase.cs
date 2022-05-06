using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;


namespace IntegrationTests;

[SetUpFixture]
public class TestDataBase : IDisposable
{

    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Company> _companiesCollection;
    private readonly IMongoCollection<Rating> _ratingsCollection;
    private readonly IMongoCollection<User> _usersCollection;
    public string _connectionString = "mongodb://localhost:27017/";
    public string _databaseName = "TestDataBase";
    private const string _companiesCollectionName = "Companies";
    private const string _ratingsCollectionName = "Ratings";
    private const string _usersCollectionName = "Users";


    public TestDataBase()
    {
        _client = new MongoClient(_connectionString);
        _database = _client.GetDatabase(_databaseName);
        _companiesCollection = _database.GetCollection<Company>(_companiesCollectionName);
        _ratingsCollection = _database.GetCollection<Rating>(_ratingsCollectionName);
        _usersCollection = _database.GetCollection<User>(_usersCollectionName);
    }

    public void AddCompanies(List<Company>companies)
    {
        List<string> ids = new List<string>();
        _companiesCollection.InsertMany(companies);
    }

    public void AddUsers(List<User>users)
    {
        _usersCollection.InsertMany(users);
    }

    public void AddRatings(List<Rating> ratings)
    {
        _ratingsCollection.InsertMany(ratings);
    }
    public void Dispose()
    {
        _client.DropDatabase(_databaseName);
    }
    
}

   