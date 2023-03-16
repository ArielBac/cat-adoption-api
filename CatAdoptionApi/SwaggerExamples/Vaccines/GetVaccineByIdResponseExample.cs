using CatAdoptionApi.Requests.Vaccines;
using CatAdoptionApi.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Vaccines
{
    public class GetVaccineByIdResponseExample : IExamplesProvider<GetVaccineRequest>
    {
        public GetVaccineRequest GetExamples()
        {
            return new GetVaccineRequest
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
            };
        }
    }
}
