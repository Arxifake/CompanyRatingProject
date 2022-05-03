using DataAccess.Interfaces;
using DataAccess.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccess.Repositories;

public class CompanyRepository:ICompaniesRepository
{
    private readonly IMongoCollection<Company> _companyTable;
    private readonly IMongoCollection<Rating> _ratingTable;


    public CompanyRepository(string connection)
    { 
        var client = new MongoClient(connection);
        var database = client.GetDatabase("CompanyRating");
        _companyTable = database.GetCollection<Company>("Companies");
        _ratingTable = database.GetCollection<Rating>("Ratings");
    }
    public List<Company> CompanyList()
    {
        return _companyTable.Find(FilterDefinition<Company>.Empty).ToList(); 
    }

    public Company GetCompanyById(string id)
    { 
        return _companyTable.Find(x => x.Id == id).FirstOrDefault();
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
                AvgGrade1=a.Average(g=>g.Grade1),AvgGrade2=a.Average(g=>g.Grade2),
                AvgGrade3=a.Average(g=>g.Grade3),AvgGrade4=a.Average(g=>g.Grade4),AvgGrade5=a.Average(g=>g.Grade5),
                AvgTotal=a.Average(g=>g.Total)
            }).ToList();
        var aggregateItem = aggregate.FirstOrDefault();
        company.Grade1Avg = Math.Round(aggregateItem.AvgGrade1,2);
        company.Grade2Avg = Math.Round(aggregateItem.AvgGrade2,2);
        company.Grade3Avg = Math.Round(aggregateItem.AvgGrade3,2);
        company.Grade4Avg = Math.Round(aggregateItem.AvgGrade4,2);
        company.Grade5Avg = Math.Round(aggregateItem.AvgGrade5,2);
        company.TotalAvg = Math.Round(aggregateItem.AvgTotal,2);
        _companyTable.ReplaceOne(c=>c.Id==id,company);
    }
}