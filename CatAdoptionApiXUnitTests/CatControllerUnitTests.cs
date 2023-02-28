using CatAdoptionApi.Data;
using CatAdoptionApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionApiXUnitTests
{
    public class CatControllerUnitTests
    {
        // Configurando dados de teste usando recurso de armazenamento em memória do entity
        private CatAdoptionContext? _context;

        public static DbContextOptions<CatAdoptionContext> dbContextOptions { get; }

        static CatControllerUnitTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<CatAdoptionContext>()
                .UseInMemoryDatabase("CatTestsDB")
                .Options;
        }
        
        public CatControllerUnitTests()
        {
            _context = new CatAdoptionContext(dbContextOptions);

            DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            db.CatSeed(_context);
        }
    }
}