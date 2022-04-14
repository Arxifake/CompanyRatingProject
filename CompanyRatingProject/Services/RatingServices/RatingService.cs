using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.Extensions.Logging;
using Services.ServicesInterfaces;


namespace Services.RatingServices;

public class RatingService:IRatingService
{
    private readonly IRatingsRepository _ratingsRepository;
    private readonly ICompaniesRepository _companiesRepository;
    private readonly ILogger<RatingService> _logger;
    private readonly IMapper _mapper;

    public RatingService(IRatingsRepository ratingsRepository,ILogger<RatingService> logger, IMapper mapper,ICompaniesRepository companiesRepository)
    {
        _ratingsRepository = ratingsRepository;
        _companiesRepository = companiesRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public RatingDto EditRate(string id)
    {
        var rate = _mapper.Map<RatingDto>(_ratingsRepository.GetRatingById(id));
        _logger.LogInformation($"User with id {rate.UserId} want to change his Rate company with id {rate.CompanyId}");
        return rate;
    }

    public void SaveRate(RatingDto rate)
    {
        _logger.LogInformation(
            $"User with id {rate.UserId} changed his Rate company with id {rate.CompanyId}");
        rate.Total = Math.Round(Convert.ToDouble(rate.Grade1 + rate.Grade2 + rate.Grade3 +
                                                 rate.Grade4 + rate.Grade5)/5,2);
        rate.DateTime = DateTime.Now;
        _ratingsRepository.ChangeRate(_mapper.Map<Rating>(rate));
        _companiesRepository.UpdateRateAvg(rate.CompanyId);
    }
}