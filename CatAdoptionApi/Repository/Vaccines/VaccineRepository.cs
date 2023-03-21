using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using CatAdoptionApi.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Vaccines
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(CatAdoptionContext context) : base(context)
        {
        }

        public async Task<PagedList<Vaccine>> GetVaccinesCat(VaccineParameters vaccineParameters)
        {
            var source = Get().Include(vaccine => vaccine.Cat).OrderBy(vaccine => vaccine.Name);

            return await PagedList<Vaccine>.ToPagedList(source, vaccineParameters.PageNumber, vaccineParameters.PageSize);
        }

        public async Task<Vaccine> GetVaccineCat(Expression<Func<Vaccine, bool>> predicate)
        {
            return await _context.Vaccines.Include(cat => cat.Cat).SingleOrDefaultAsync(predicate);
        }
    }
}
