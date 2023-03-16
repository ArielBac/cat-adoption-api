using CatAdoptionApi.Models;
using CatAdoptionApi.Requests.Vaccines;
using CatAdoptionApi.ViewModels;
using Swashbuckle.AspNetCore.Filters;
using System.Xml.Linq;

namespace CatAdoptionApi.SwaggerExamples.Vaccines
{
    public class GetVaccineResponseExample : IExamplesProvider<List<GetVaccineRequest>>
    {
        public List<GetVaccineRequest> GetExamples()
        {
            return new List<GetVaccineRequest>
            {
                new GetVaccineRequest
                {
                    Id = 1,
                    CatId = 1,
                    Name = "Vacina 1",
                    Producer = "Fabricante 1",
                    Applied_at = DateTime.Parse("2023-03-16T16:25:18"),
                    Cat = new CatViewModel
                    {
                        Id = 1,
                        Name = "Pimpolho",
                        Breed = "Viralata",
                        Weight = 2,
                        Color = "Marrom",
                        Age = 3,
                        Gender = "M"
                    }
                },
                new GetVaccineRequest
                {
                    Id = 2,
                    CatId = 2,
                    Name = "Vacina 2",
                    Producer = "Fabricante 2",
                    Applied_at = DateTime.Parse("2023-03-16T16:25:18"),
                    Cat = new CatViewModel
                    {
                        Id = 2,
                        Name = "Josefina",
                        Breed = "Viralata",
                        Weight = 5,
                        Color = "Preto",
                        Age = 3,
                        Gender = "F"
                    }
                }
            };
        }
    }
}
