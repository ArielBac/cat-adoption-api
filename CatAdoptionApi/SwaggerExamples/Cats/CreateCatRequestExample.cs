using CatAdoptionApi.Requests.Cats;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Cats
{
    public class CreateCatRequestExample : IExamplesProvider<CreateCatRequest>
    {
        public CreateCatRequest GetExamples()
        {
            return new CreateCatRequest
            {
                Name = "Jonas",
                Breed = "Viralata",
                Weight = 4.5,
                Color = "Branco",
                Age = 2,
                Gender = "M",
            };
        }
    }
}
