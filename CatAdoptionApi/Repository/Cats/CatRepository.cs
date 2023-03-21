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

        public async Task<PagedList<Cat>> GetCatsVaccines(CatParameters catParameters)
        {
            var source = Get().Include(cat => cat.Vaccines).OrderBy(cat => cat.Name);
          
            return await PagedList<Cat>.ToPagedList(source, catParameters.PageNumber, catParameters.PageSize);
        }

        public async Task<Cat> GetCatVaccines(Expression<Func<Cat, bool>> predicate)
        {
            return await _context.Cats.Include(cat => cat.Vaccines).SingleOrDefaultAsync(predicate);
        }
    }
}
