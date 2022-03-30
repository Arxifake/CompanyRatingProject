using System.Collections.Generic;
using AutoMapper;
using Castle.Core.Internal;
using DataAccess.Interfaces;
using DataAccess.Models;
using Moq;
using NUnit.Framework;
using Services.HomeServices;
using Services.ServicesInterfaces;
using Mapper = DTO.Mapper;

namespace NUnitTestsCompanyRateProject;

public class Tests
{
    private IHomeService _homeService;
    [SetUp]
    public void Setup()
    {
        var mockCompanies = new Mock<ICompaniesRepository>();
        var mockAuthors = new Mock<IAuthorsRepository>();
        var mockNickname = new Mock<INicknameRepository>();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new Mapper());
        });
        IMapper mapper = new AutoMapper.Mapper(mappingConfig);
        List<Company> companies = new List<Company>()
        {
            new Company() {Name = "1"}, new Company() {Name = "2"},
            new Company() {Name = "3"}, new Company() {Name = "4"},
            new Company() {Name = "5"}
        };
        mockCompanies.Setup(a => a.CompanyList()).Returns(companies);
        _homeService = new HomeService(mockCompanies.Object,mockAuthors.Object,mockNickname.Object,mapper);
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