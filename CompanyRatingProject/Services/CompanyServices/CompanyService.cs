using AutoMapper;
using CompanyRatingProject.Models;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.ServicesInterfaces;

namespace Services.CompanyServices;
public class CompanyService:ICompanyService
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IRatingsRepository _ratingsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<CompanyService> _logger;
    private readonly IMapper _mapper;

     public CompanyService(ICompaniesRepository companiesRepository, IRatingsRepository ratingsRepository, IUsersRepository usersRepository,IMapper mapper, ILogger<CompanyService> logger)
    {
        _companiesRepository = companiesRepository;
        _ratingsRepository = ratingsRepository;
        _usersRepository=usersRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public CompanyRateModelView GetCompanyRateView(string id, HttpContext context)
    {
        CompanyRateModelView companyRate = new CompanyRateModelView();
        companyRate.Company = _mapper.Map<CompanyDto>(_companiesRepository.GetCompanyById(id));
        if (companyRate.Company.Name!=null)
        {
            _logger.LogInformation($"Show company with id = {id}");
            companyRate.Company.Ratings = 
                _mapper.Map<List<RatingDto>>(_ratingsRepository.GetRatingsByCompanyId(id));
            foreach (var rating in companyRate.Company.Ratings)
            {
                rating.User = _mapper.Map<UserDto>(_usersRepository.GetUserById(rating.UserId));
            }

            try
            {
                var userId = context.Request.Cookies["user_id"];
                var userRaiting = companyRate.Company.Ratings.First(rating => rating.UserId.Equals(userId));
                companyRate.Rating = userRaiting;
                _logger.LogInformation("add User Rate with User Id " + $"{userId}");
            }
            catch
            {
                companyRate.Rating = new RatingDto();
                companyRate.Rating.CompanyId = companyRate.Company.Id;
                _logger.LogInformation("User didn't rate company yet");
            }
        
            return companyRate;
        }
        else
        {
            _logger.LogInformation("The user tried to open a page with a non-existent company");
            throw new CompanyNotFoundException();
        }
    }

    public CompanyRateModelView PostCompanyRateView( HttpContext context,RatingDto rating)
    {
        CompanyRateModelView companyRate = new CompanyRateModelView();
        companyRate.Rating = rating;
        // ReSharper disable once PossibleLossOfFraction
        companyRate.Rating.Total = Math.Round(Convert.ToDouble(companyRate.Rating.Grade1 + companyRate.Rating.Grade2 + companyRate.Rating.Grade3 +
                                    companyRate.Rating.Grade4 + companyRate.Rating.Grade5)/5,2);
        companyRate.Rating.UserId = context.Request.Cookies["user_id"];
        companyRate.Rating.DateTime = DateTime.Now;
        _ratingsRepository.AddRate(_mapper.Map<Rating>(companyRate.Rating));
        _companiesRepository.UpdateRateAvg(rating.CompanyId);
        _logger.LogInformation($"User with id= {companyRate.Rating.UserId} add Rate to company {companyRate.Rating.CompanyId}");
        return companyRate;
    }
    public void CreateCompany(CompanyDto company, HttpContext context)
    {
        _logger.LogInformation($"User with id {context.Request.Cookies["user_id"]} create new company {company.Name}");
        _companiesRepository.AddCompany(_mapper.Map<Company>(company));
    }

    public CompanyDto GetCompany(string id)
    {
        return _mapper.Map<CompanyDto>(_companiesRepository.GetCompanyById(id));
    }

    public void SaveCompany(CompanyDto company, HttpContext context)
    {
        _logger.LogInformation($"User with id {context.Request.Cookies["user_id"]} changed company {company.Name}");
        _companiesRepository.EditCompany(_mapper.Map<Company>(company));
    }
    public void DeleteCompany(string id, HttpContext context)
    {
        _logger.LogInformation($"User with id {context.Request.Cookies["user_id"]} delete company with id {id}");
        _companiesRepository.DeleteCompany(id);
    }

    
}

