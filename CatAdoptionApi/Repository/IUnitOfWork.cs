using CatAdoptionApi.Repository.Cats;
using CatAdoptionApi.Repository.Vaccines;

namespace CatAdoptionApi.Repository
{
    public interface IUnitOfWork
    {
        ICatRepository CatRepository { get; }
        IVaccineRepository VaccineRepository { get; }
        Task Commit();
    }
}
