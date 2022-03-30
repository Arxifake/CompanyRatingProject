using AutoMapper;
using DataAccess.Models;
using DTO.ModelViewsObjects;

namespace DTO;

public class Mapper:Profile
{
    public Mapper()
    {
        CreateMap<CompanyDto,Company>().ReverseMap();
        CreateMap<Rating,RatingDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
    }
}