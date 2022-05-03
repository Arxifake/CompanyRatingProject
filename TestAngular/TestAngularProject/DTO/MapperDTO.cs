using AutoMapper;
using DataAccess.Models;
using DTO.ModelViewsObjects;

namespace DTO;

public class MapperDto:Profile
{
    public MapperDto()
    {
        CreateMap<CompanyDto,Company>().ReverseMap();
        CreateMap<CompanyMainDto, Company>().ReverseMap();
        CreateMap<Rating,RatingDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}