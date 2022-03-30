using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;
using Services.ServicesInterfaces;

namespace Services.CompanyServices;
public class CompanyService:ICompanyService
{
    private readonly ICompaniesRepository _companies;
    private readonly IRatingsRepository _ratings;
    private readonly IAuthorsRepository _authors;
    private readonly IMapper _mapper;

     public CompanyService(ICompaniesRepository companies, IRatingsRepository ratings, IAuthorsRepository authors,IMapper mapper)
    {
        _companies = companies;
        _ratings = ratings;
        _authors = authors;
        _mapper = mapper;
    }

    public CompanyRateModelView GetCompanyRateView(int id, HttpContext context)
    {
        
        CompanyRateModelView companyRate = new CompanyRateModelView();
        companyRate.Company = _mapper.Map<CompanyDto>(_companies.GetCompanyById(id));
        companyRate.Company.Ratings = 
            _mapper.Map<List<RatingDto>>(_ratings.GetRatingsByCompanyId(id));
        foreach (var rating in companyRate.Company.Ratings)
        {
            rating.Author = _mapper.Map<AuthorDto>(_authors.GetAuthorById(rating.AuthorId));
        }

        try
        {
            var userId =Int32.Parse(context.Request.Cookies["user_id"]) ;
            var userRaiting = companyRate.Company.Ratings.First(rating => rating.AuthorId.Equals(userId));
            companyRate.Rating = userRaiting;
        }
        catch
        {
            companyRate.Rating = new RatingDto();
        }
        
        return companyRate;
    }

    public CompanyRateModelView PostCompanyRateView(int id, HttpContext context,RatingDto rating)
    {
        CompanyRateModelView companyRate = new CompanyRateModelView();
        companyRate.Company = _mapper.Map<CompanyDto>(_companies.GetCompanyById(id));
        companyRate.Company.Ratings = 
            _mapper.Map<List<RatingDto>>(_ratings.GetRatingsByCompanyId(id));
        foreach (var companyRating in companyRate.Company.Ratings)
        {
            companyRating.Author = _mapper.Map<AuthorDto>(_authors.GetAuthorById(rating.AuthorId));
        }

        companyRate.Rating = rating;
        companyRate.Rating.CompanyId = companyRate.Company.Id;
        // ReSharper disable once PossibleLossOfFraction
        companyRate.Rating.Total = Math.Round(Convert.ToDouble(companyRate.Rating.Grade1 + companyRate.Rating.Grade2 + companyRate.Rating.Grade3 +
                                    companyRate.Rating.Grade4 + companyRate.Rating.Grade5)/5,2);
        companyRate.Rating.AuthorId = Int32.Parse(context.Request.Cookies["user_id"]);
        companyRate.Rating.DateTime = DateTime.Now;
        _ratings.AddRate(_mapper.Map<Rating>(companyRate.Rating));
        return companyRate;
    }

    public RatingDto EditRate(int id)
    {
        return _mapper.Map<RatingDto>(_ratings.GetRatingById(id));
    }

    public void SaveRate(RatingDto rate)
    {
        rate.Total = Math.Round(Convert.ToDouble(rate.Grade1 + rate.Grade2 + rate.Grade3 +
                                                               rate.Grade4 + rate.Grade5)/5,2);
        rate.DateTime = DateTime.Now;
        _ratings.ChangeRate(_mapper.Map<Rating>(rate));
    }
    public void CreateCompany(CompanyDto company)
    {
        _companies.AddCompany(_mapper.Map<Company>(company));
    }

    public CompanyDto GetCompany(int id)
    {
        return _mapper.Map<CompanyDto>(_companies.GetCompanyById(id));
    }

    public void SaveCompany(CompanyDto company)
    {
        _companies.EditCompany(_mapper.Map<Company>(company));
    }
    public void DeleteCompany(int id)
    {
        _companies.DeleteCompany(id);
    }

    public void AddRatings()
    {
        for (int i = 48; i <= 1033; i++)
        {
            for (int j = 0; j < 30; j++)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[30];
                Random random = new Random();
                int comp = random.Next(16, 20000);
                int r1 = random.Next(1, 5);
                int r2 = random.Next(1, 5);
                int r3 = random.Next(1, 5);
                int r4 = random.Next(1, 5);
                int r5 = random.Next(1, 5);
                double total = Math.Round(Convert.ToDouble(r1 + r2 + r3 + r4 + r5)/5,2);
                DateTime dateTime = DateTime.Now;
                for (int a = 0; a < stringChars.Length; a++)
                {
                    stringChars[a] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringChars);
                Rating rating = new Rating()
                {
                    Grade1 = r1,Grade2 = r2,Grade3 = r3,Grade4 = r4,Grade5 = r5,Total = total,Text = finalString,
                    CompanyId = comp,AuthorId = i,DateTime = dateTime
                };
                _ratings.AddRate(rating);
            }
        }
    }
}