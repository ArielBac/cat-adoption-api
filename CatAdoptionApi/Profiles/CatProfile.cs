using AutoMapper;
using CatAdoptionApi.Data.Dtos.Cats;
using CatAdoptionApi.Models;
using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Profiles;

public class CatProfile : Profile
{
    public CatProfile()
    {
        CreateMap<CreateCatDto, Cat>();
        CreateMap<UpdateCatDto, Cat>();
        CreateMap<Cat, ReadCatDto>();
        CreateMap<Cat, UpdateCatDto>();
        CreateMap<Cat, CatViewModel>();
    }
}
