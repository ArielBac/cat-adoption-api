using CatAdoptionApi.Data;
using CatAdoptionApi.Repository.Cats;
using CatAdoptionApi.Repository.Vaccines;

namespace CatAdoptionApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private CatRepository _catRepository;
        private VaccineRepository _vaccineRepository;
        public CatAdoptionContext _context;

        public UnitOfWork(CatAdoptionContext context)
        {
            _context = context;
        }

        public ICatRepository CatRepository
        {
            get
            {
                // Se nula, passo o contexto, senão passo a instância que já existe
                return _catRepository = _catRepository ?? new CatRepository(_context);
            }
        }

        public IVaccineRepository VaccineRepository
        {
            get
            {
                return _vaccineRepository = _vaccineRepository ?? new VaccineRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
