using CatAdoptionApi.Models;
using CatAdoptionApi.Pagination;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Cats
{
    public interface ICatRepository : IRepository<Cat>
    {
        PagedList<Cat> GetCatsVaccines(CatParameters catParameters);

        Cat GetCatVaccines(Expression<Func<Cat, bool>> predicate);


    }
}
