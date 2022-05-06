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
    private int i = 0;
    private int j = 0;
    private int k = 0;

    private static List<Company> Companies()
    {
        List<Company> companies = new List<Company>()
        {
            new Company(){Id = "111111111111111111111111",Name = "First",Description = "FirstDesc"},
            new Company(){Id = "222222222222222222222222",Name = "Second",Description = "SecondDesc"},
            new Company(){Id = "333333333333333333333333",Name = "Third",Description = "ThirdDesc"}
        };
        return companies;
    }
    private static List<Company> CompaniesForEdit()
    {
        List<Company> companies = new List<Company>()
        {
            new Company(){Id = "111111111111111111111111",Name = "FirstEdit",Description = "FirstDesc"},
            new Company(){Id = "222222222222222222222222",Name = "SecondEdit",Description = "SecondDesc"},
            new Company(){Id = "333333333333333333333333",Name = "ThirdEdit",Description = "ThirdDesc"}
        };
        return companies;
    }
    private static List<Company> CompaniesForAdd()
    {
        List<Company> companies = new List<Company>()
        {
            new Company(){Id = "444444444444444444444444",Name = "FirstAdd",Description = "FirstDesc"},
            new Company(){Id = "555555555555555555555555",Name = "SecondAdd",Description = "SecondDesc"},
            new Company(){Id = "666666666666666666666666",Name = "ThirdAdd",Description = "ThirdDesc"}
        };
        return companies;
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        _base = new TestDataBase();
        _base.AddCompanies(Companies());
        _companiesRepository = new CompanyRepository("mongodb://localhost:27017/",_base._databaseName);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _base.Dispose();
    }
    

    [Test]
    public void ACompanyList_GetCompanyListFromDB_GetList()
    {
        var companiesList = Companies();
        var companyList = _companiesRepository.CompanyList();
        companyList.Reverse();
        Assert.AreEqual(companiesList.First().Name,companyList.First().Name);
        Assert.AreEqual(companiesList.Last().Name,companyList.Last().Name);
        Assert.AreEqual(companiesList.Count(),companyList.Count());
    }

    [Test]
    [TestCase("111111111111111111111111")]
    [TestCase("222222222222222222222222")]
    [TestCase("333333333333333333333333")]
    public void BGetCompanyById_GetCompany_GetCompanyWithRightId(string id)
    {
        
        var companiesList = Companies();
        var getCompanyById = _companiesRepository.GetCompanyById(id);
        Assert.AreEqual(getCompanyById.Name,companiesList[i].Name);
        i++;
    }

    [Test]
    [TestCaseSource(nameof(CompaniesForEdit))]

    public void CEditCompany_ChangeName_NameChanged(Company company)
    {
        
        _companiesRepository.EditCompany(company);
        var editCompany = _companiesRepository.GetCompanyById(company.Id);
        Assert.AreEqual(editCompany.Name,CompaniesForEdit()[j].Name);
        Assert.AreNotEqual(editCompany.Name,Companies()[j].Name);
        j++;
    }
    
    [Test]
    [TestCase("111111111111111111111111")]
    [TestCase("222222222222222222222222")]
    public void DeleteCompany_Delete_LastCompaniesIdNotEquals(string id)
    {
        _companiesRepository.DeleteCompany(id);
        var companyList = _companiesRepository.CompanyList();
        companyList.Reverse();
        Assert.AreNotEqual(CompaniesForEdit()[k].Name,companyList.First().Name);
        Assert.AreEqual(CompaniesForEdit()[k+1].Name,companyList.First().Name);
        k++;
    }
    [Test]
    [TestCaseSource(nameof(CompaniesForAdd))]
    public void ECreateCompany_Create_CompanyAddToDb(Company company)
    {
        _companiesRepository.AddCompany(company);
        var companies = _companiesRepository.CompanyList();
        companies.Reverse();
        Assert.AreEqual(companies.Last().Name,company.Name);
    }

    
}