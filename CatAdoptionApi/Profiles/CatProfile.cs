﻿using AutoMapper;
using CatAdoptionApi.Models;
using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Profiles;

public class CatProfile : Profile
{
    public CatProfile()
    {
        CreateMap<CreateCatRequest, Cat>();
        CreateMap<Cat, GetCatRequest>();
        CreateMap<Cat, UpdateCatRequest>().ReverseMap();
        CreateMap<Cat, CatViewModel>();
        
        //CreateMap<CreateCatDto, Cat>();
        //CreateMap<UpdateCatDto, Cat>();
        //CreateMap<Cat, ReadCatDto>();
        //CreateMap<Cat, UpdateCatDto>();
        //CreateMap<Cat, CatViewModel>();
    }
}
