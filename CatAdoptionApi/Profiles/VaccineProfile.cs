using AutoMapper;
using CatAdoptionApi.Data.Dtos.Vaccines;
using CatAdoptionApi.Models;
using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.Requests.Vaccines;
using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Profiles;

public class VaccineProfile : Profile
{
    public VaccineProfile() 
    {
        CreateMap<Vaccine, GetVaccineRequest>();
        CreateMap<CreateVaccineRequest, Vaccine>();
        CreateMap<UpdateVaccineRequest, Vaccine>();
        CreateMap<Vaccine, UpdateVaccineRequest>();
        CreateMap<Vaccine, VaccineViewModel>();
        
        //CreateMap<Vaccine, ReadVaccineDto>();
        //CreateMap<CreateVaccineDto, Vaccine>();
        //CreateMap<UpdateVaccineDto, Vaccine>();
        //CreateMap<Vaccine, UpdateVaccineDto>();
        //CreateMap<Vaccine, VaccineViewModel>();



    }

}
