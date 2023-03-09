using AutoMapper;
using CatAdoptionApi.Controllers;
using CatAdoptionApi.Data;
using CatAdoptionApi.Pagination;
using CatAdoptionApi.Profiles;
using CatAdoptionApi.Repository;
using CatAdoptionApi.Requests.Vaccines;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionApiXUnitTests
{
    public class VaccineControllerUnitTests
    {
        // Configurando dados de teste usando recurso de armazenamento em memória do entity
        private IUnitOfWork _unitOfWork;
        private CatAdoptionContext _context;
        private IMapper _mapper;

        public static DbContextOptions<CatAdoptionContext> dbContextOptions { get; }

        static VaccineControllerUnitTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<CatAdoptionContext>()
                .UseInMemoryDatabase("VaccineTestsDB")
                .Options;
        }

        public VaccineControllerUnitTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VaccineProfile());
                cfg.AddProfile(new CatProfile());
            });
            _mapper = config.CreateMapper();

            _context = new CatAdoptionContext(dbContextOptions);

            DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();

            db.CatSeed(_context);
            db.VaccineSeed(_context);

            _unitOfWork = new UnitOfWork(_context);
        }

        // ======================================== Index action =============================================
        // TODO
        // Arrumar testes de index, pois depois da paginação, pararam de funcionar
        [Fact]
        public async Task Index_Return_OkResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };
            var vaccineParameters = new VaccineParameters();

            // Act
            var data = await controller.Index(vaccineParameters);

            // Assert
            Assert.IsType<List<GetVaccineRequest>>(data.Value);
        }

        [Fact]
        // Como não inicializei o HttpContext, vou ter uma exceção
        public async Task Index_Return_BadRequestResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineParameters = new VaccineParameters();

            // Act
            var data = await controller.Index(vaccineParameters);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        [Fact]
        public async Task Index_MatchResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };
            var vaccineParameters = new VaccineParameters();

            // Act
            var data = await controller.Index(vaccineParameters);

            // Assert
            Assert.IsType<List<GetVaccineRequest>>(data.Value);

            var vaccine = data.Value.Should().BeAssignableTo<List<GetVaccineRequest>>().Subject;

            // O retorno é ordenado por Nome, em ordem alfabética
            Assert.Equal("v5", vaccine[4].Name);
            Assert.Equal("Fabricante 1", vaccine[4].Producer);
            Assert.Equal(DateTime.Parse("2020-05-12T15:30:00"), vaccine[4].Applied_at);
            Assert.Equal(1, vaccine[4].CatId);
            Assert.Equal("Pingo", vaccine[4].Cat.Name);

            Assert.Equal("v4", vaccine[1].Name);
            Assert.Equal("Fabricante 3", vaccine[1].Producer);
            Assert.Equal(DateTime.Parse("2020-04-12T14:20:00"), vaccine[1].Applied_at);
            Assert.Equal(3, vaccine[1].CatId);
            Assert.Equal("Joana", vaccine[1].Cat.Name);
        }

        // ======================================== Show action =============================================
        [Fact]
        public async Task Show_Return_OkResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 2;

            // Act
            var data = await controller.Show(vaccineId);

            // Assert
            Assert.IsType<GetVaccineRequest>(data.Value);
        }

        [Fact]
        public async Task Show_Return_NotFoundResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 10;

            // Act
            var data = await controller.Show(vaccineId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact(Skip = "Para este teste passar, é preciso lançar uma exceção no controller")]
        public async Task Show_Return_BadRequestResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 1;

            // Act
            var data = await controller.Show(vaccineId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        [Fact]
        public async Task Show_MatchResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 4;

            // Act
            var data = await controller.Show(vaccineId);

            // Assert
            Assert.IsType<GetVaccineRequest>(data.Value);

            var vaccine = data.Value;

            Assert.Equal(vaccineId, vaccine.Id);
            Assert.Equal("v4", vaccine.Name);
            Assert.Equal("Fabricante 3", vaccine.Producer);
            Assert.Equal(DateTime.Parse("2020-04-12T14:20:00"), vaccine.Applied_at);
        }
        // ============================================ Create action ==============================================
        [Fact]
        public async Task Create_Return_CreatedResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineRequest = new CreateVaccineRequest { Name = "Vacina de teste create", Producer = "Fabricante de teste", Applied_at = DateTime.Parse("2022-08-12T17:32:00"), CatId = 5 };

            // Act
            var data = await controller.Create(vaccineRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public async Task Create_Return_BadRequestResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineRequest = new CreateVaccineRequest { Producer = "Fabricante de teste", Applied_at = DateTime.Parse("2022-08-12T17:32:00"), CatId = 20 };

            // Act
            var data = await controller.Create(vaccineRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }

        // ============================================ Update action ==============================================
        [Fact]
        public async Task Update_Return_NoContentResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineRequest = new UpdateVaccineRequest { Name = "Vacina de teste update", Producer = "Fabricante de teste", Applied_at = DateTime.Parse("2022-08-12T17:32:00") };
            var vaccineId = 2;

            // Act
            var data = await controller.Update(vaccineId, vaccineRequest);

            // Assert
            Assert.IsType<NoContentResult>(data);
        }

        [Fact]
        public async Task Update_Return_NotFoundResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineRequest = new UpdateVaccineRequest { Name = "Vacina de teste update", Producer = "Fabricante de teste", Applied_at = DateTime.Parse("2022-08-12T17:32:00") };
            var vaccineId = 10;

            // Act
            var data = await controller.Update(vaccineId, vaccineRequest);

            // Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact(Skip = "Para este teste passar, é preciso lançar uma exceção no controller")]
        public async Task Update_Return_BadRequestResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineRequest = new UpdateVaccineRequest { Name = "Vacina de teste update" };
            var vaccineId = 2;

            // Act
            var data = await controller.Update(vaccineId, vaccineRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async Task Update_MatchResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineRequest = new UpdateVaccineRequest { Name = "Vacina de teste update", Producer = "Fabricante de teste", Applied_at = DateTime.Parse("2022-08-12T17:32:00") };
            var vaccineId = 3;

            // Act
            var data = await controller.Update(vaccineId, vaccineRequest);

            // Assert
            Assert.IsType<NoContentResult>(data);

            var vaccineUpdated = await _unitOfWork.VaccineRepository.GetById(vaccine => vaccine.Id == vaccineId);

            Assert.Equal(vaccineId, vaccineUpdated.Id);
            Assert.Equal(vaccineRequest.Name, vaccineUpdated.Name);
            Assert.Equal(vaccineRequest.Producer, vaccineUpdated.Producer);
            Assert.Equal(vaccineRequest.Applied_at, vaccineUpdated.Applied_at);
        }

        // ============================================ PartialUpdate action ========================================
        [Fact]
        public async Task PartialUpdate_Return_NoContentResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 2;
            JsonPatchDocument<UpdateVaccineRequest> patch = new JsonPatchDocument<UpdateVaccineRequest>();
            patch.Replace(vaccine => vaccine.Name, "v5 parcialmente atualizado");
            patch.Replace(vaccine => vaccine.Producer, "Fabricante 1 parcialmente atualizado");

            // Act
            var data = await controller.PartialUpdate(vaccineId, patch);

            // Assert
            Assert.IsType<NoContentResult>(data);
        }

        // ============================================ Destroy action ==============================================
        [Fact]
        public async Task Destroy_Return_NoContentResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 3;

            // Act
            var data = await controller.Destroy(vaccineId);

            // Assert
            Assert.IsType<NoContentResult>(data);
        }

        [Fact]
        public async Task Destroy_Return_NotFoundResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 10;

            // Act
            var data = await controller.Destroy(vaccineId);

            // Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact(Skip = "Para este teste passar, é preciso lançar uma exceção no controller")]
        public async Task Destroy_Return_BadRequestResult()
        {
            // Arrange
            var controller = new VaccineController(_unitOfWork, _mapper);
            var vaccineId = 1;

            // Act
            var data = await controller.Destroy(vaccineId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }
    }
}
