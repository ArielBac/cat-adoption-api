using AutoMapper;
using CatAdotionApi.Data.Dtos;
using CatAdotionApi.Models;

namespace CatAdotionApi.Profiles;

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
