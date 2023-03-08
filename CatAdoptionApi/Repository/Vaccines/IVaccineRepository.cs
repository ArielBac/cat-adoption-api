using CatAdoptionApi.Models;
using CatAdoptionApi.Pagination;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Vaccines
{
    public interface IVaccineRepository : IRepository<Vaccine>
    {
        PagedList<Vaccine> GetVaccinesCat(VaccineParameters vaccineParameters);
        Vaccine GetVaccineCat(Expression<Func<Vaccine, bool>> predicate);
    }
}
