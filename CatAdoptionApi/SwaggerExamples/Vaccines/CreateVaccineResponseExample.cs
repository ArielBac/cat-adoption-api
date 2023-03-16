using CatAdoptionApi.Requests.Vaccines;
using CatAdoptionApi.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Vaccines
{
    public class CreateVaccineResponseExample : IExamplesProvider<GetVaccineRequest>
    {
        public GetVaccineRequest GetExamples()
        {
            return new GetVaccineRequest
            {
                Id = 2,
                CatId = 2,
                Name = "Vacina 2",
                Producer = "Fabricante 2",
                Applied_at = DateTime.Parse("2023-03-16T16:25:18"),
                Cat = null
            };
        }
    }
}
