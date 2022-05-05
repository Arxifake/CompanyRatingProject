using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using DTO.ModelViewsObjects;
using IntegrationTests;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Services.CompanyServices;
using Services.ServicesInterfaces;

namespace UnitTests;

public class CompanyServiceTests
{
    private TestDataBase _base;
    private ICompanyService _companyService;
    private Mock<ICompaniesRepository> _mockCompaniesRepository;
    private Mock<IRatingsRepository> _mockRatingsRepository;
    private Mock<IUsersRepository> _usersRepositoryMock;
    private Mock<ILogger<CompanyService>> _loggerMock;
    private IMapper _mapper;
    private HttpContext _httpContext = new DefaultHttpContext();
    
    private List<Company> Companies()
    {
        List<Company> companies = new List<Company>()
        {
            new Company() {Id = "1",Name = "1"}, new Company() {Id = "2",Name = "2"},
            new Company() {Id = "4",Name = "3"}, new Company() {Id = "4",Name = "4"},
            new Company() {Id = "5",Name = "5"}
        };
        return companies;
    }

    private List<Rating> Ratings()
    {
        List<Rating> ratings = new List<Rating>()
        {
            new Rating(){Id = "1",CompanyId = "2", UserId = "1"},new Rating(){Id = "2",CompanyId = "2",UserId = "2"},
            new Rating(){Id = "3",CompanyId = "1",UserId = "3"},new Rating(){Id = "4",CompanyId = "1",UserId = "1"},
            new Rating(){Id = "5",CompanyId = "1",UserId = "4"},new Rating(){Id = "6",CompanyId = "4",UserId = "6"}
        };
        return ratings;
    }
    private List<User> Users()
    {
        List<User> users = new List<User>()
        {
            new User(){Id = "1"},new User(){Id = "2"},
            new User(){Id = "3"},new User(){Id = "4"},
            new User(){Id = "5"},new User(){Id = "6"}
        };
        return users;
    }
    private static List<RatingDto> RatingsForAdd()
    {
        List<RatingDto> ratings = new List<RatingDto>()
        {
            new RatingDto(){Grade1 = 3,Grade2 = 2,Grade3 = 2,Grade4 = 5,Grade5 = 4,CompanyId = "2"},
            new RatingDto(){Grade1 = 3,Grade2 = 2,Grade3 = 1,Grade4 = 3,Grade5 = 4,CompanyId = "3"},
            new RatingDto(){Grade1 = 3,Grade2 = 5,Grade3 = 4,Grade4 = 5,Grade5 = 1,CompanyId = "1"}
        };
        return ratings;
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        _mockCompaniesRepository = new Mock<ICompaniesRepository>();
        _mockRatingsRepository = new Mock<IRatingsRepository>();
        _usersRepositoryMock = new Mock<IUsersRepository>();
        _loggerMock = new Mock<ILogger<CompanyService>>();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperDTO());
        });
        _mapper = new Mapper(mappingConfig);
        _mockCompaniesRepository.Setup(m => m.GetCompanyById(It.IsAny<string>()))
            .Returns<string>((i) => Companies().First(c => c.Id == i));
        _mockRatingsRepository.Setup(m => m.GetRatingsByCompanyId(It.IsAny<string>()))
            .Returns<string>((i) => Ratings().FindAll(a => a.CompanyId == i));
        _mockRatingsRepository.Setup(m => m.AddRate(It.IsAny<Rating>()))
            .Callback<Rating>((rating) => { Ratings().Add(rating); });
        _usersRepositoryMock.Setup(m => m.GetUserById(It.IsAny<string>()))
            .Returns<string>((id) => Users().First(u => u.Id == id));
        _companyService = new CompanyService(_mockCompaniesRepository.Object,_mockRatingsRepository.Object,_usersRepositoryMock.Object,_mapper,_loggerMock.Object);
    }
    [Test]
    [TestCase("2")]
    [TestCase("1")]
    [TestCase("4")]
    public void GetCompanyRateView_GetCompany_GetCompanyWithIdAndRatingsForCompany(string id)
    {
       var getCompany = _companyService.GetCompanyRateView(id,_httpContext);
       var ratingsList = _mapper.Map<List<RatingDto>>(Ratings().FindAll(r => r.CompanyId == id));
       var resultRatings = getCompany.Company.Ratings.ToList();
       Assert.AreEqual(getCompany.Company.Id,id);
       Assert.AreEqual(resultRatings.First().Id,ratingsList.First().Id);
       Assert.AreEqual(resultRatings.Last().Id,ratingsList.Last().Id);
       Assert.AreEqual(resultRatings.Count(),ratingsList.Count());
    }
    
}