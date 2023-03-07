using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Cats
{
    public class CatRepository : Repository<Cat>, ICatRepository
    {
        public CatRepository(CatAdoptionContext context) : base(context)
        {
        }

        public IEnumerable<Cat> GetCatsVaccines()
        {
            return Get().Include(cat => cat.Vaccines);
        }

        public Cat GetCatVaccines(Expression<Func<Cat, bool>> predicate)
        {
            return _context.Cats.Include(cat => cat.Vaccines).SingleOrDefault(predicate);
        }
    }
}
