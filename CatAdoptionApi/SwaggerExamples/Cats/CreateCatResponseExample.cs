using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.ViewModels;
using Swashbuckle.AspNetCore.Filters;

namespace CatAdoptionApi.SwaggerExamples.Cats
{
    public class CreateCatResponseExample : IExamplesProvider<GetCatRequest>
    {
        public GetCatRequest GetExamples()
        {
            return new GetCatRequest
            {
                Id = 3,
                Name = "Jonas",
                Breed = "Viralata",
                Weight = 4.5,
                Color = "Branco",
                Age = 2,
                Gender = "M",
                Vaccines = new List<VaccineViewModel>()
            };
        }
    }
}
