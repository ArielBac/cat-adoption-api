using AutoMapper;
using CatAdoptionApi.Models;
using CatAdoptionApi.Requests.Vaccines;
using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Profiles;

public class VaccineProfile : Profile
{
    public VaccineProfile() 
    {
        CreateMap<Vaccine, GetVaccineRequest>();
        CreateMap<CreateVaccineRequest, Vaccine>();
        CreateMap<Vaccine, UpdateVaccineRequest>().ReverseMap();
        CreateMap<Vaccine, VaccineViewModel>();
        
        //CreateMap<Vaccine, ReadVaccineDto>();
        //CreateMap<CreateVaccineDto, Vaccine>();
        //CreateMap<UpdateVaccineDto, Vaccine>();
        //CreateMap<Vaccine, UpdateVaccineDto>();
        //CreateMap<Vaccine, VaccineViewModel>();



    }

}
