using CatAdoptionApi.Requests.Vaccines;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Vaccines
{
    public class CreateVaccineRequestExample : IExamplesProvider<CreateVaccineRequest>
    {
        public CreateVaccineRequest GetExamples()
        {
            return new CreateVaccineRequest
            {
                CatId = 1,
                Applied_at = DateTime.Parse("2023-03-16T16:25:18"),
                Name = "v5",
                Producer = "Fabricante 1"
            };
        }
    }
}
