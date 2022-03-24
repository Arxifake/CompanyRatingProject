using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;
using Services.ServicesInterfaces;

namespace Services.CompanyControllerServices;

public class CompanyService:ICompanyService
{
    private readonly ICompaniesRepository _companies;
    private readonly IRatingsRepository _ratings;
    private readonly IAuthorsRepository _authors;

     public CompanyService(ICompaniesRepository companies, IRatingsRepository ratings, IAuthorsRepository authors)
    {
        _companies = companies;
        _ratings = ratings;
        _authors = authors;
    }

    public CompanyRateModelView GetCompanyRateView(int id)
    {
        CompanyRateModelView companyRate = new CompanyRateModelView();
        companyRate.Company = _companies.GetCompanyById(id);
        companyRate.Company.Ratings = _ratings.GetRatingsByCompanyId(id);
        _authors.AuthorList();
        foreach (var rating in companyRate.Company.Ratings)
        {
            rating.Author = _authors.GetAuthorById(rating.AuthorId);
        }
        companyRate.Rating = new Rating();
        return companyRate;
    }

    public CompanyRateModelView PostCompanyRateView(int id, HttpContext context)
    {
        CompanyRateModelView companyRate = new CompanyRateModelView();
        companyRate.Company = _companies.GetCompanyById(id);
        companyRate.Company.Ratings = _ratings.GetRatingsByCompanyId(id);
        _authors.AuthorList();
        foreach (var rating in companyRate.Company.Ratings)
        {
            rating.Author = _authors.GetAuthorById(rating.AuthorId);
        }
        companyRate.Rating.CompanyId = companyRate.Company.Id;
        // ReSharper disable once PossibleLossOfFraction
        companyRate.Rating.Total = (companyRate.Rating.Grade1 + companyRate.Rating.Grade2 + companyRate.Rating.Grade3 +
                                    companyRate.Rating.Grade4 + companyRate.Rating.Grade5)/5;
        companyRate.Rating.AuthorId = Int32.Parse(context.Request.Cookies["user_id"]);
        companyRate.Rating.DateTime = DateTime.Now;
        companyRate.Rating.Text = "";
        _ratings.AddRate(companyRate.Rating);
        return companyRate;
    }
}