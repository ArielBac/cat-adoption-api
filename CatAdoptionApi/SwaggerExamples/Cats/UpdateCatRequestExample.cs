using CatAdoptionApi.Requests.Cats;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Cats
{
    public class UpdateCatRequestExample : IExamplesProvider<UpdateCatRequest>
    {
        public UpdateCatRequest GetExamples()
        {
            return new UpdateCatRequest
            {
                Name = "Jonas atualizado",
                Breed = "Viralata",
                Weight = 4,
                Color = "Branco",
                Age = 3,
                Gender = "M",
            };
        }
    }
}
