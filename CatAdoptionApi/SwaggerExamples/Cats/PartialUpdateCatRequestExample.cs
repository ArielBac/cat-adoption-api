using CatAdoptionApi.Requests.Cats;
using Microsoft.AspNetCore.JsonPatch;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;

namespace CatAdoptionApi.SwaggerExamples.Cats
{
    public class PartialUpdateCatRequestExample : IExamplesProvider<JsonPatchDocument<UpdateCatRequest>>
    {
        public JsonPatchDocument<UpdateCatRequest> GetExamples()
        {
            JsonPatchDocument<UpdateCatRequest> patch = new JsonPatchDocument<UpdateCatRequest>();
            patch.Replace(cat => cat.Name, "Pimpolho atualizado");
            patch.Replace(cat => cat.Age, 6);

            return patch;
        }
    }
}
