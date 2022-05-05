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
    public Page ShowCompanies(string? top, string? searchString, int? pageNumber)
    {
        _logger.LogInformation("Enter in ShowCompanies method");
        
        var companiesList = new List<CompanyDto>();
        switch(top)
        {
            case "all":
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList()).ToList();
                break;
            case "top10":
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList().Take(10)).ToList();
                break;
                
            case "top25":
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList().Take(25)).ToList();
                break;
                
            case "top50":
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList().Take(50)).ToList();
                break;
            
            default:
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companiesRepository.CompanyList()).ToList(); 
                break;
            
        }
        if (!String.IsNullOrEmpty(searchString))
        {
            companiesList = companiesList.Where(s => s.Name.Contains(searchString)).ToList();
        }
        _logger.LogInformation($"Return {companiesList.Count()} objects ");
         var pagination = Pagination<CompanyDto>.Create(companiesList, pageNumber ?? 1, _pageSize,top, searchString);
         var list = new Page();
         list.Companies = pagination;
         
         list.HasPrevPage= pagination.HasPrevPage;
         list.HasNextPage= pagination.HasNextPage;
         list.PageIndex= pagination.PageIndex;
         list.TotalPages= pagination.TotalPages;
         list.PageSize = pagination.PageSize;
         list.Top = pagination.Top;
         list.SearchString = pagination.SearchString;
         return list;

    }
    
}