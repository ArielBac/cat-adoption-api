using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using System.Linq.Expressions;

namespace CatAdoptionApi.Repository.Vaccines
{
    public class VaccineRepository : Repository<Vaccine>, IVaccineRepository
    {
        public VaccineRepository(CatAdoptionContext context) : base(context)
        {
        }
    }
}
