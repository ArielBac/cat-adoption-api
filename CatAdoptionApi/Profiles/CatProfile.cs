using AutoMapper;
using CatAdoptionApi.Data.Dtos;
using CatAdoptionApi.Models;

namespace CatAdoptionApi.Profiles;

public class CatProfile : Profile
{
    public CatProfile()
    {
        CreateMap<CreateCatDto, Cat>();
        CreateMap<UpdateCatDto, Cat>();
        CreateMap<Cat, ReadCatDto>();
        CreateMap<Cat, UpdateCatDto>();
    }
}
