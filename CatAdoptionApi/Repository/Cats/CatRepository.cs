using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using CatAdoptionApi.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Cats
{
    public class CatRepository : Repository<Cat>, ICatRepository
    {
        public CatRepository(CatAdoptionContext context) : base(context)
        {
        }

        public PagedList<Cat> GetCatsVaccines(CatParameters catParameters)
        {
            var source = Get().Include(cat => cat.Vaccines).OrderBy(cat => cat.Name);
          
            return PagedList<Cat>.ToPagedList(source, catParameters.PageNumber, catParameters.PageSize);
        }

        public Cat GetCatVaccines(Expression<Func<Cat, bool>> predicate)
        {
            return _context.Cats.Include(cat => cat.Vaccines).SingleOrDefault(predicate);
        }
    }
}
