using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Cats
{
    public class GetCatResponseExample : IExamplesProvider<List<GetCatRequest>>
    {
        public List<GetCatRequest> GetExamples()
        {
            return new List<GetCatRequest> 
            { 
                new GetCatRequest
                {
                    Id = 1,
                    Name = "Pimpolho",
                    Breed = "Viralata",
                    Weight = 2,
                    Color = "Marrom",
                    Age = 3,
                    Gender = "M",
                    Vaccines = new List<VaccineViewModel>
                    {
                        new VaccineViewModel
                        {
                            Id = 1,
                            Name = "Vacina 1",
                            Producer = "Fabricante 1",
                            Applied_at = DateTime.Parse("2023-03-16T16:25:18")
                        }
                    }
                },
                new GetCatRequest
                {
                    Id = 2,
                    Name = "Josefina",
                    Breed = "Viralata",
                    Weight = 5,
                    Color = "Preto",
                    Age = 3,
                    Gender = "F",
                    Vaccines = new List<VaccineViewModel>
                    {
                        new VaccineViewModel
                        {
                            Id = 2,
                            Name = "Vacina 2",
                            Producer = "Fabricante 2",
                            Applied_at = DateTime.Parse("2023-03-16T16:25:18")
                        }
                    }
                },
            };
        }
    }
}
