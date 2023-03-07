using CatAdoptionApi.Models;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Vaccines
{
    public interface IVaccineRepository : IRepository<Vaccine>
    {
        IEnumerable<Vaccine> GetVaccinesCat();
        Vaccine GetVaccineCat(Expression<Func<Vaccine, bool>> predicate);
    }
}
