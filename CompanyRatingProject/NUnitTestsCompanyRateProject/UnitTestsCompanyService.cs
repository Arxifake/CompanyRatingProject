using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using Services.CompanyServices;
using Services.ServicesInterfaces;

namespace NUnitTestsCompanyRateProject;

public class UnitTestsCompanyService
{
    private ICompanyService _companyService;
    [SetUp]
    public void SetUp()
    {
        var mockCompanies = new Mock<ICompaniesRepository>();
        var mockRatings = new Mock <IRatingsRepository>();
        var mockAuthors = new Mock<IAuthorsRepository>();
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DTO.Mapper());
        });
        List<Company> companies = new List<Company>()
        {
            new Company() {Id = 1}, new Company() {Id = 2},
            new Company() {Id = 3}, new Company() {Id = 4},
            new Company() {Id = 5}
        };
        List<Rating> ratings = new List<Rating>()
        {
            new Rating() {CompanyId = 1,AuthorId = 3}, new Rating() {CompanyId = 2,AuthorId = 2},
            new Rating(){CompanyId = 1,AuthorId = 1},new Rating(){CompanyId = 3,AuthorId = 3},
            new Rating(){CompanyId = 4,AuthorId = 1},new Rating(){CompanyId = 3,AuthorId = 4}
        };
        List<Author> authors = new List<Author>()
        {
            new Author(){Id = 1},new Author(){Id = 2},
            new Author(){Id = 3},new Author(){Id = 4},
            new Author(){Id = 5}
        };
        mockCompanies.Setup(m => m.GetCompanyById(It.IsAny<int>()))
            .Returns<int>(id=>companies.First(c =>c.Id.Equals(id)));
        mockRatings.Setup(m => m.GetRatingsByCompanyId(It.IsAny<int>()))
            .Returns<int>(id => ratings.FindAll(r =>r.CompanyId.Equals(id)));
        mockRatings.Setup(m => m.AddRate(It.IsAny<Rating>()))
            .Callback<Rating>(rate => ratings.Add(rate));
        mockAuthors.Setup(m => m.GetAuthorById(It.IsAny<int>()))
            .Returns<int>(id => authors.First(a => a.Id.Equals(id)));
        IMapper mapper = new AutoMapper.Mapper(mappingConfig);
        _companyService = new CompanyService(mockCompanies.Object,mockRatings.Object,mockAuthors.Object,mapper);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(5)]
    public void GetCompanyRateView_CompanyId_ReturnCompanyWithThatId(int id)
    {
        var companyRate = _companyService.GetCompanyRateView(id);
        Assert.AreEqual(companyRate.Company.Id,id);
    }

    [Test]
    public void PostCompanyRateView_NewRate_AddToRatingsNewRate(int id, HttpContext context, RatingDto rating)
    {
        var companyRate = _companyService.PostCompanyRateView(id, context, rating);
        Assert.True(companyRate.Company.Ratings.Contains(rating));
        
    }
}