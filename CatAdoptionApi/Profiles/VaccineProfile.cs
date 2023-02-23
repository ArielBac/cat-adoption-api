using AutoMapper;
using CatAdoptionApi.Data.Dtos.Vaccines;
using CatAdoptionApi.Models;
using CatAdoptionApi.ViewModels;

namespace CatAdoptionApi.Profiles;

public class VaccineProfile : Profile
{
    public VaccineProfile() 
    {
        CreateMap<Vaccine, ReadVaccineDto>();
        CreateMap<ReadVaccineDto, Vaccine>();
        CreateMap<CreateVaccineDto, Vaccine>();
        CreateMap<UpdateVaccineDto, Vaccine>();
        CreateMap<Vaccine, UpdateVaccineDto>();
        CreateMap<Vaccine, VaccineViewModel>();
    }

}
