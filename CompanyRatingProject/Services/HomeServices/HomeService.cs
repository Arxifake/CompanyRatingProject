using AutoMapper;
using DataAccess.Interfaces;
using DataAccess.Models;
using DTO.ModelViewsObjects;
using Microsoft.AspNetCore.Http;
using Services.ServicesInterfaces;

namespace Services.HomeServices;

public class HomeService:IHomeService
{
    private readonly ICompaniesRepository _companies;
    private readonly IAuthorsRepository _authors;
    private readonly IMapper _mapper;
    private readonly INicknameRepository _nickname;
    private int _pageSize = 25;
    

     public HomeService(ICompaniesRepository companies, IAuthorsRepository authors, INicknameRepository nickname,IMapper mapper)
    {
        _companies = companies;
        _authors = authors;
        _nickname = nickname;
        _mapper = mapper;
    }
    public Pagination<CompanyDto> ShowCompanies(string top, string current, string searchString, int? pageNumber)
    {
        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = current;
        }
        IEnumerable<CompanyDto> companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList());
        switch(top)
        {
            case "all":
                
                companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList());
                break;
            case "top10":
                try
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList().GetRange(0, 10));
                    break;
                }
                catch
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList());
                    break;
                }
            case "top25":
                try
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList().GetRange(0, 25));
                    break;
                }
                catch
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList());
                    break;
                }
            case "top50":
                try
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList().GetRange(0, 50));
                    break;
                }
                catch
                {
                    companiesList = _mapper.Map<IEnumerable<CompanyDto>>(_companies.CompanyList());
                    break;
                }
        }
        if (!String.IsNullOrEmpty(searchString))
        {
            companiesList = companiesList.Where(s => s.Name.Contains(searchString));
        }
        
        return Pagination<CompanyDto>.Create(companiesList, pageNumber ?? 1, _pageSize);
         
    }

    public void CheckUser(HttpContext context)
    {
        if (!context.Request.Cookies.ContainsKey("user_id"))
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddMonths(12);
            Author author = new Author();
            //Nickname name = _nickname.GetNickname();
            //author.Nickname =name.Name ;
            author.Nickname = "MishaTest";
            _authors.NewAuthor(author);
            context.Response.Cookies.Append("user_id",_authors.AuthorList().Last().Id.ToString(),cookieOptions);
        }
        
    }

    public void AddCompanies()
    {
        for (int i = 0; i < 100000; i++)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[12];
            var DescChars = new char[20];
            var random = new Random();

            for (int j = 0; j < stringChars.Length; j++)
            {
                stringChars[j] = chars[random.Next(chars.Length)];
            }
            for (int j = 0; j < DescChars.Length; j++)
            {
                DescChars[j] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            var finalDesc = new String(DescChars);
            Company newCompany = new Company() {Name = finalString,Description =finalDesc };
            _companies.AddCompany(newCompany);    
        }
        
    }
}