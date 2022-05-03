using AutoMapper;
using DataAccess.Models;
using DTO.ModelViewsObjects;

namespace DTO;

public class MapperDTO:Profile
{
    public MapperDTO()
    {
        CreateMap<CompanyDto,Company>().ReverseMap();
        CreateMap<CompanyMainDto, Company>().ReverseMap();
        CreateMap<CompanyCreateDto, Company>().ReverseMap();
        CreateMap<Rating,RatingDto>().ReverseMap();
        CreateMap<Rating, RateDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}