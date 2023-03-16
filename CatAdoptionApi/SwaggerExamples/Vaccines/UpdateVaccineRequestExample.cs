using CatAdoptionApi.Requests.Vaccines;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Vaccines
{
    public class UpdateVaccineRequestExample : IExamplesProvider<UpdateVaccineRequest>
    {
        public UpdateVaccineRequest GetExamples()
        {
            return new UpdateVaccineRequest
            {
                Applied_at = DateTime.Parse("2023-04-16T13:25:18"),
                Name = "v5",
                Producer = "Fabricante 1"
            };
        }
    }
}
