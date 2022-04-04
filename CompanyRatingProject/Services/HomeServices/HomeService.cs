using AutoMapper;
using DataAccess.Interfaces;
using DTO.ModelViewsObjects;
using Microsoft.Extensions.Logging;
using Services.ServicesInterfaces;

namespace Services.HomeServices;

public class HomeService:IHomeService
{
    private readonly ICompaniesRepository _companiesRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<HomeService> _logger;
    private int _pageSize = 25;
    

     public HomeService(ICompaniesRepository companiesRepository,IMapper mapper,ILogger<HomeService> logger)
    {
        _companiesRepository = companiesRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public Pagination<CompanyDto> ShowCompanies(string top, string current, string searchString, int? pageNumber)
    {
        _logger.LogInformation("Enter in ShowCompanies method");
        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = current;
        }
        IEnumerable<CompanyDto> companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList());
        switch(top)
        {
            case "all":
                
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList());
                break;
            case "top10":
                try
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList().GetRange(0, 10));
                    break;
                }
                catch
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList());
                    break;
                }
            case "top25":
                try
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList().GetRange(0, 25));
                    break;
                }
                catch
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList());
                    break;
                }
            case "top50":
                try
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList().GetRange(0, 50));
                    break;
                }
                catch
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList());
                    break;
                }
        }
        if (!String.IsNullOrEmpty(searchString))
        {
            companiesList = companiesList.Where(s => s.Name.Contains(searchString));
        }
        _logger.LogInformation($"Return {companiesList.Count()} objects ");
        return Pagination<CompanyDto>.Create(companiesList, pageNumber ?? 1, _pageSize);
         
    }
}