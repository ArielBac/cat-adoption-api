using CatAdoptionApi.Requests.Vaccines;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Vaccines
{
    public class PartialUpdateVaccineRequestExample : IExamplesProvider<JsonPatchDocument<UpdateVaccineRequest>>
    {
        public JsonPatchDocument<UpdateVaccineRequest> GetExamples()
        {
            JsonPatchDocument<UpdateVaccineRequest> patch = new JsonPatchDocument<UpdateVaccineRequest>();
            patch.Replace(vaccine => vaccine.Name, "v5 parcialmente atualizada");
            patch.Replace(vaccine => vaccine.Producer, "Fabricante 1 parcialmente atualizado");

            return patch;
        }
    }
}
