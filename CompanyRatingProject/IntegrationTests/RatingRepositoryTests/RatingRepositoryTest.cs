using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using NUnit.Framework;

namespace IntegrationTests.RatingRepositoryTests;

public class RatingRepositoryTest
{
    private IRatingsRepository _ratingsRepository;
    private TestDataBase _base;

    private List<Rating> Ratings()
    {
        List<Rating> ratings = new List<Rating>()
        {
            new Rating() {Id = "111111111111111111111111",Grade1 = 2, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 2,DateTime = DateTime.Now,CompanyId = "1",UserId = "2"},
            new Rating() {Id = "222222222222222222222222",Grade1 = 3, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 3,DateTime = DateTime.Now,CompanyId = "1",UserId = "1"},
            new Rating() {Id = "333333333333333333333333",Grade1 = 4, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 4,DateTime = DateTime.Now,CompanyId = "2",UserId = "3"}
        };
        return ratings;
    }

    private static List<Rating> RatingsForEdit()
    {
        List<Rating> ratings = new List<Rating>()
        {
            new Rating() {Id = "111111111111111111111111",Grade1 = 2, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 2,Text = "Add",DateTime = DateTime.Now,CompanyId = "1",UserId = "2"},
            new Rating() {Id = "222222222222222222222222",Grade1 = 3, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 3,Text = "Edit",DateTime = DateTime.Now,CompanyId = "1",UserId = "1"},
            new Rating() {Id = "333333333333333333333333",Grade1 = 4, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 4,Text = "",DateTime = DateTime.Now,CompanyId = "2",UserId = "3"}
        };
        return ratings;
    }

    private static List<Rating> RatingsForAdd()
    {
        List<Rating> ratings = new List<Rating>()
        {
            new Rating() {Grade1 = 2, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 2,Text = "Add",DateTime = DateTime.Now,CompanyId = "3",UserId = "2"},
            new Rating() {Grade1 = 3, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 3,Text = "Edit",DateTime = DateTime.Now,CompanyId = "3",UserId = "1"},
            new Rating() {Grade1 = 4, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 4,Text = "",DateTime = DateTime.Now,CompanyId = "2",UserId = "4"}
        };
        return ratings;
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        _base = new TestDataBase();
        _base.AddRatings(Ratings());
        _ratingsRepository = new RatingRepository("mongodb://localhost:27017/",_base._databaseName);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _base.Dispose();
    }

    [Test]
    [TestCase("1")]
    [TestCase("2")]
    public void AGetRatingsByCompanyId_GetRatings_WithRightCompanyId(string id)
    {
        var ratings = _ratingsRepository.GetRatingsByCompanyId(id);
        Assert.AreEqual(ratings[0].CompanyId,id);
        
    }
    [Test]
    [TestCase("111111111111111111111111")]
    [TestCase("222222222222222222222222")]
    public void BGetRatingById_GetRating_GetRatingWithRightId(string id)
    {
        var rating = _ratingsRepository.GetRatingById(id);
        var exceptRating = Ratings().First(x => x.Id == id);
        Assert.AreEqual(rating.CompanyId,exceptRating.CompanyId);
        Assert.AreEqual(rating.UserId,exceptRating.UserId);
    }
    [Test]
    [TestCaseSource(nameof(RatingsForEdit))]
    public void CEditRate_Edit_ChangeComment(Rating rating)
    {
        _ratingsRepository.ChangeRate(rating);
        var ratingEdit = _ratingsRepository.GetRatingById(rating.Id);
        var exceptRating = Ratings().First(x => x.Id == rating.Id);
        Assert.AreNotEqual(ratingEdit.Text,exceptRating.Text);
        Assert.AreEqual(ratingEdit.Text,rating.Text);
    }

    [Test]
    [TestCaseSource(nameof(RatingsForAdd))]
    public void DAddRate_Add_RateAddToDb(Rating rating)
    {
        _ratingsRepository.AddRate(rating);
        var ratings = _ratingsRepository.GetRatingsByCompanyId(rating.CompanyId);
        Assert.AreEqual(rating.Text,ratings.Last().Text);
    }
}