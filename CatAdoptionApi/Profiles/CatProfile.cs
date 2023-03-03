using AutoMapper;
using CatAdoptionApi.Data.Dtos.Cats;
using CatAdoptionApi.Models;
using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Profiles;

public class CatProfile : Profile
{
    public CatProfile()
    {
        CreateMap<Cat, GetCatRequest>();
        CreateMap<CreateCatRequest, Cat>();
        CreateMap<UpdateCatRequest, Cat>();
        CreateMap<Cat, UpdateCatRequest>();
        CreateMap<Cat, CatViewModel>();
        
        //CreateMap<CreateCatDto, Cat>();
        //CreateMap<UpdateCatDto, Cat>();
        //CreateMap<Cat, ReadCatDto>();
        //CreateMap<Cat, UpdateCatDto>();
        //CreateMap<Cat, CatViewModel>();

   

    }
}
