using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Vaccines
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(CatAdoptionContext context) : base(context)
        {
        }

        public IEnumerable<Vaccine> GetVaccinesCat()
        {
            return Get().Include(vaccine => vaccine.Cat);
        }

        public Vaccine GetVaccineCat(Expression<Func<Vaccine, bool>> predicate)
        {
            return _context.Vaccines.Include(cat => cat.Cat).SingleOrDefault(predicate);
        }
    }
}
