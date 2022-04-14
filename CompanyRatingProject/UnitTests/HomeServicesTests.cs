using System.Collections.Generic;
using AutoMapper;
using Castle.Core.Internal;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.HomeServices;
using Services.ServicesInterfaces;
using Mapper = DTO.MapperDTO;

namespace UnitTests;

public class HomeServicesTests
{
    private IHomeService _homeService;

    private List<Company> Companies()
    {
        List<Company> companies = new List<Company>()
        {
            new Company() {Name = "1"}, new Company() {Name = "2"},
            new Company() {Name = "3"}, new Company() {Name = "4"},
            new Company() {Name = "5"}
        };
        return companies;
    }
    [SetUp]
    public void Setup()
    {
        var mockCompanies = new Mock<ICompaniesRepository>();
        var mockLogger = new Mock<ILogger<HomeService>>();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperDTO());
        });
        IMapper mapper = new AutoMapper.Mapper(mappingConfig);
        
        mockCompanies.Setup(a => a.CompanyList()).Returns(Companies());
        _homeService = new HomeService(mockCompanies.Object,mapper,mockLogger.Object);
    }

    [Test]
    [TestCase(null,null,null,null)]
    [TestCase("all",null,null,3)]
    [TestCase("top10",null,null,null)]
    [TestCase(null,null,"2",null)]
    public void ShowCompanies_PaginationTerms_ShouldReturnRightCompanies(string top, string current, string searchString, int? pageNumber)
    {
        var companies =_homeService.ShowCompanies(top, current, searchString, pageNumber);
        var y = (((companies.PageIndex - 1) * companies.PageSize) + 1).ToString();
        if (!searchString.IsNullOrEmpty())
        {
            y = searchString;
        }
        
        Assert.AreEqual(companies[0].Name,y);
    }
}