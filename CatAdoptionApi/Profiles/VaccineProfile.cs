using AutoMapper;
using CatAdoptionApi.Data.Dtos.Vaccines;
using CatAdoptionApi.Data.ViewModels;
using CatAdoptionApi.Models;

namespace CatAdoptionApi.Profiles
{
    public class VaccineProfile : Profile
    {
        public VaccineProfile() 
        {
            CreateMap<Vaccine, ReadVaccineDto>();
            CreateMap<ReadVaccineDto, Vaccine>();
            CreateMap<Vaccine, VaccineViewModel>();
            CreateMap<CreateVaccineDto, Vaccine>();
        }

    }
}
