using CatAdoptionApi.Models;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Cats
{
    public interface ICatRepository : IRepository<Cat>
    {
        IEnumerable<Cat> GetCatsVaccines();

        Cat GetCatVaccines(Expression<Func<Cat, bool>> predicate);
    }
}
