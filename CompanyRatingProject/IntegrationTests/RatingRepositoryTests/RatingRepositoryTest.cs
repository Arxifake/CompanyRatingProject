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
            new Rating() {Grade1 = 2, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 2,DateTime = DateTime.Now,CompanyId = 1,UserId = 2},
            new Rating() {Grade1 = 3, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 3,DateTime = DateTime.Now,CompanyId = 1,UserId = 1},
            new Rating() {Grade1 = 4, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 4,DateTime = DateTime.Now,CompanyId = 2,UserId = 3}
        };
        return ratings;
    }

    private static List<Rating> RatingsForEdit()
    {
        List<Rating> ratings = new List<Rating>()
        {
            new Rating() {Id = 1,Grade1 = 2, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 2,Text = "Add",DateTime = DateTime.Now,CompanyId = 1,UserId = 2},
            new Rating() {Id = 2,Grade1 = 3, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 3,Text = "Edit",DateTime = DateTime.Now,CompanyId = 1,UserId = 1},
            new Rating() {Id = 3,Grade1 = 4, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 4,Text = "",DateTime = DateTime.Now,CompanyId = 2,UserId = 3}
        };
        return ratings;
    }

    private static List<Rating> RatingsForAdd()
    {
        List<Rating> ratings = new List<Rating>()
        {
            new Rating() {Grade1 = 2, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 2,Text = "Add",DateTime = DateTime.Now,CompanyId = 3,UserId = 2},
            new Rating() {Grade1 = 3, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 3,Text = "Edit",DateTime = DateTime.Now,CompanyId = 3,UserId = 1},
            new Rating() {Grade1 = 4, Grade2 = 2, Grade3 = 2, Grade4 = 2, Grade5 = 3, Total = 4,Text = "",DateTime = DateTime.Now,CompanyId = 2,UserId = 4}
        };
        return ratings;
    }

    [OneTimeSetUp]
    public void SetUp()
    {
        _base = new TestDataBase();
        _base.Init();
        _base.CreateDatabase();
        _base.CreateTableRatings();
        _base.CreateProceduresForRatings();
        _base.InsertIntoRatings(Ratings());
        _ratingsRepository = new RatingRepository(_base._connectionString);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _base.DeleteDb();
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public void GetRatingsByCompanyId_GetRatings_WithRightCompanyId(int id)
    {
        var ratings = _ratingsRepository.GetRatingsByCompanyId(id);
        Assert.AreEqual(ratings[0].CompanyId,id);
        
    }
    [Test]
    [TestCase(1)]
    [TestCase(2)]
    public void GetRatingById_GetRating_GetRatingWithRightId(int id)
    {
        var rating = _ratingsRepository.GetRatingById(id);
        Assert.AreEqual(rating.CompanyId,Ratings()[id-1].CompanyId);
        Assert.AreEqual(rating.UserId,Ratings()[id-1].UserId);
    }
    [Test]
    [TestCaseSource(nameof(RatingsForEdit))]
    public void EditRate_Edit_ChangeComment(Rating rating)
    {
        _ratingsRepository.ChangeRate(rating);
        var ratingEdit = _ratingsRepository.GetRatingById(rating.Id);
        Assert.AreNotEqual(ratingEdit.Text,Ratings()[rating.Id-1].Text);
    }

    [Test]
    [TestCaseSource(nameof(RatingsForAdd))]
    public void AddRate_Add_RateAddToDb(Rating rating)
    {
        _ratingsRepository.AddRate(rating);
        var ratings = _ratingsRepository.GetRatingsByCompanyId(rating.CompanyId);
        Assert.AreEqual(rating.Text,ratings.Last().Text);
    }
}