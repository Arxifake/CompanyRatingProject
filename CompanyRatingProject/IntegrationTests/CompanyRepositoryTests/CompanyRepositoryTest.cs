using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using NUnit.Framework;

namespace IntegrationTests.CompanyRepositoryTests;

public class CompanyRepositoryTest
{
    private ICompaniesRepository _companiesRepository;
    private TestDataBase _base;

    private List<Company> Companies()
    {
        List<Company> companies = new List<Company>()
        {
            new Company(){Name = "First",Description = "FirstDesc"},
            new Company(){Name = "Second",Description = "SecondDesc"},
            new Company(){Name = "Third",Description = "ThirdDesc"}
        };
        return companies;
    }
    private static List<Company> CompaniesForEdit()
    {
        List<Company> companies = new List<Company>()
        {
            new Company(){Id = 1,Name = "FirstEdit",Description = "FirstDesc"},
            new Company(){Id = 2,Name = "SecondEdit",Description = "SecondDesc"},
            new Company(){Id = 3,Name = "ThirdEdit",Description = "ThirdDesc"}
        };
        return companies;
    }
    private static List<Company> CompaniesForAdd()
    {
        List<Company> companies = new List<Company>()
        {
            new Company(){Name = "FirstAdd",Description = "FirstDesc"},
            new Company(){Name = "SecondAdd",Description = "SecondDesc"},
            new Company(){Name = "ThirdAdd",Description = "ThirdDesc"}
        };
        return companies;
    }
    [OneTimeSetUp]
    public void SetUp()
    {
        _base = new TestDataBase();
        _base.Init();
        _base.CreateDatabase();
        _base.CreateTableCompanies();
        _base.CreateProceduresForCompanies();
        _base.InsertIntoCompanies(Companies());
        _companiesRepository = new CompanyRepository(_base._connectionString);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _base.DeleteDb();
    }

    [Test]
    public void CompanyList_GetCompanyListFromDB_GetList()
    {
        var companiesList = Companies();
        var companyList = _companiesRepository.CompanyList();
        companyList.Reverse();
        Assert.AreEqual(companiesList.First().Name,companyList.First().Name);
        Assert.AreEqual(companiesList.Last().Name,companyList.Last().Name);
        Assert.AreEqual(companiesList.Count(),companyList.Count());
    }

    [Test]
    [TestCase(1)]
    [TestCase(3)]
    [TestCase(2)]
    public void GetCompanyById_GetCompany_GetCompanyWithRightId(int id)
    {
        var companiesList = Companies();
        var getCompanyById = _companiesRepository.GetCompanyById(id);
        Assert.AreEqual(getCompanyById.Name,companiesList[id-1].Name);
    }

    [Test]
    [TestCaseSource(nameof(CompaniesForEdit))]

    public void EditCompany_ChangeName_NameChanged(Company company)
    {
        _companiesRepository.EditCompany(company);
        var editCompany = _companiesRepository.GetCompanyById(company.Id);
        Assert.AreEqual(editCompany.Name,CompaniesForEdit()[company.Id-1].Name);
        Assert.AreNotEqual(editCompany.Name,Companies()[company.Id-1].Name);
    }
    
    [Test]
    [TestCase(1)]
    public void DeleteCompany_Delete_LastCompaniesIdNotEquals(int id)
    {
        _companiesRepository.DeleteCompany(id);
        var companyList = _companiesRepository.CompanyList();
        companyList.Reverse();
        Assert.AreNotEqual(Companies()[id-1].Name,companyList[id-1].Name);
        Assert.AreEqual(Companies()[id].Name,companyList[id-1].Name);
    }

    [Test]
    [TestCaseSource(nameof(CompaniesForAdd))]
    public void CreateCompany_Create_CompanyAddToDb(Company company)
    {
        _companiesRepository.AddCompany(company);
        var companies = _companiesRepository.CompanyList();
        companies.Reverse();
        Assert.AreEqual(companies.Last().Name,company.Name);
    }
}