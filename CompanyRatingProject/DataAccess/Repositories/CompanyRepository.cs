using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class CompanyRepository:ICompaniesRepository
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private IMongoCollection<Company> _companyTable;
    private IMongoCollection<Rating> _ratingTable;


    public CompanyRepository(string connection)
    { 
        _client = new MongoClient(connection);
        _database = _client.GetDatabase("CompanyRating");
        _companyTable = _database.GetCollection<Company>("Companies");
        _ratingTable = _database.GetCollection<Rating>("Ratings");
    }
    public List<Company> CompanyList()
    {
        return _companyTable.Find(FilterDefinition<Company>.Empty).ToList(); 
    }

    public Company GetCompanyById(string id)
    { 
        return _companyTable.Find(x => x.Id == id).First();
    }

    public void DeleteCompany(string id)
    { 
        _companyTable.DeleteOne(x=>x.Id==id);
    }

    public void EditCompany(Company company)
    {
        var update = Builders<Company>.Update.Set(x=>x.Name,company.Name).Set(x=>x.Description,company.Description);
        _companyTable.UpdateOne(x => x.Id == company.Id,update);
    }

    public void AddCompany(Company company)
    { 
        company.Id = ObjectId.GenerateNewId().ToString(); 
        _companyTable.InsertOne(company);
    }

    public void UpdateRateAvg(string id)
    {
        var company = GetCompanyById(id);
        var aggregate = _ratingTable.Aggregate().Match(r=>r.CompanyId==company.Id).Group(r=>r.CompanyId,
            a=>new
            {
                CompanyId=a.Key,AvgGrade1=a.Average(g=>g.Grade1),AvgGrade2=a.Average(g=>g.Grade2),
                AvgGrade3=a.Average(g=>g.Grade3),AvgGrade4=a.Average(g=>g.Grade4),AvgGrade5=a.Average(g=>g.Grade5),
                AvgTotal=a.Average(g=>g.Total)
            }).ToList();
        company.Grade1Avg = Math.Round(aggregate.Select(_ => _.AvgGrade1).First(),2);
        company.Grade2Avg = Math.Round(aggregate.Select(_ => _.AvgGrade2).First(),2);
        company.Grade3Avg = Math.Round(aggregate.Select(_ => _.AvgGrade3).First(),2);
        company.Grade4Avg = Math.Round(aggregate.Select(_ => _.AvgGrade4).First(),2);
        company.Grade5Avg = Math.Round(aggregate.Select(_ => _.AvgGrade5).First(),2);
        company.TotalAvg = Math.Round(aggregate.Select(_ => _.AvgTotal).First(),2);
        _companyTable.ReplaceOne(c=>c.Id==id,company);
    }
}