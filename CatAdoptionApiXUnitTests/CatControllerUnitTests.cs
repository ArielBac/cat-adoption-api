using AutoMapper;
using CatAdoptionApi.Controllers;
using CatAdoptionApi.Data;
using CatAdoptionApi.Profiles;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.Repository;
using Microsoft.AspNetCore.JsonPatch;
using CatAdoptionApi.Pagination;
using Microsoft.AspNetCore.Http;

namespace CatAdoptionApiXUnitTests
{
    public class CatControllerUnitTests
    {
        // Configurando dados de teste usando recurso de armazenamento em mem�ria do entity
        private IUnitOfWork _unitOfWork;
        private CatAdoptionContext _context;
        private IMapper _mapper;

        public static DbContextOptions<CatAdoptionContext> dbContextOptions { get; }

        static CatControllerUnitTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<CatAdoptionContext>()
                .UseInMemoryDatabase("CatTestsDB")
                .Options;
        }

        public CatControllerUnitTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CatProfile());
            });
            _mapper = config.CreateMapper();

            _context = new CatAdoptionContext(dbContextOptions);

            DBUnitTestsMockInitializer db = new DBUnitTestsMockInitializer();
            db.CatSeed(_context);

            _unitOfWork = new UnitOfWork(_context);
        }

        // ======================================== Index action =============================================
        // TODO
        // Arrumar testes de index, pois depois da pagina��o, pararam de funcionar
        //[Fact(Skip="Depois da pagina��o, parou de funcionar")]
        [Fact]
        public void Index_Return_OkResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };
            var catParameters = new CatParameters();
            catParameters.PageNumber = 1;
            catParameters.PageSize = 10;

            // Act
            var data = controller.Index(catParameters);

            // Assert
            Assert.IsType<List<GetCatRequest>>(data.Value);
        }

        [Fact]
        // Como n�o inicializei o HttpContext, vou ter uma exce��o
        public void Index_Return_BadRequestResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catParameters = new CatParameters();

            // Act
            var data = controller.Index(catParameters);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        [Fact]
        public void Index_MatchResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext(),
            };
            var catParameters = new CatParameters();

            // Act
            var data = controller.Index(catParameters);

            // Assert
            Assert.IsType<List<GetCatRequest>>(data.Value);

            var cat = data.Value.Should().BeAssignableTo<List<GetCatRequest>>().Subject;

            // O retorno � ordenado por Nome, em ordem alfab�tica
            Assert.Equal("Gabi", cat[0].Name);
            Assert.Equal("Viralata", cat[0].Breed);
            Assert.Equal(4.5, cat[0].Weight);
            Assert.Equal(9, cat[0].Age);
            Assert.Equal("Marrom", cat[0].Color);
            Assert.Equal("F", cat[0].Gender);

            Assert.Equal("Joana", cat[1].Name);
            Assert.Equal("Viralata", cat[1].Breed);
            Assert.Equal(3.5, cat[1].Weight);
            Assert.Equal(3, cat[1].Age);
            Assert.Equal("Branco", cat[1].Color);
            Assert.Equal("F", cat[1].Gender);
        }

        // ======================================== Show action =============================================
        [Fact]
        public void Show_Return_OkResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 2;

            // Act
            var data = controller.Show(catId);

            // Assert
            Assert.IsType<GetCatRequest>(data.Value);
        }

        [Fact]
        public void Show_Return_NotFoundResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 10;

            // Act
            var data = controller.Show(catId);

            // Assert
            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact(Skip = "Para este teste passar, � preciso lan�ar uma exce��o no controller")]
        public void Show_Return_BadRequestResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 1;

            // Act
            var data = controller.Show(catId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data.Result);
        }

        [Fact]
        public void Show_MatchResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 3;

            // Act
            var data = controller.Show(catId);

            // Assert
            Assert.IsType<GetCatRequest>(data.Value);

            var cat = data.Value;

            Assert.Equal(catId, cat.Id);
            Assert.Equal("Joana", cat.Name);
            Assert.Equal("Viralata", cat.Breed);
            Assert.Equal(3.5, cat.Weight);
            Assert.Equal(3, cat.Age);
            Assert.Equal("F", cat.Gender);
        }

        // ============================================ Create action ==============================================
        [Fact]
        public void Create_Return_CreatedResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catRequest = new CreateCatRequest { Name = "Fuleco", Age = 1, Breed = "Viralata", Color = "Preto", Gender = "M", Weight = 3.5 };

            // Act
            var data = controller.Create(catRequest);

            // Assert
            Assert.IsType<CreatedAtActionResult>(data);
        }

        [Fact]
        public void Create_Return_BadRequestResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catRequest = new CreateCatRequest { Age = 1, Color = "Preto", Gender = "M", Weight = 3.5 };

            // Act
            var data = controller.Create(catRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }

        // ============================================ Update action ==============================================
        [Fact]
        public void Update_Return_NoContentResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catRequest = new UpdateCatRequest { Name = "Jonas atualizado", Age = 1, Breed = "Viralata", Color = "Preto", Gender = "M", Weight = 3.5 };
            var catId = 2;

            // Act
            var data = controller.Update(catId, catRequest);

            // Assert
            Assert.IsType<NoContentResult>(data);
        }

        [Fact]
        public void Update_Return_NotFoundResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catRequest = new UpdateCatRequest { Name = "Jonas atualizado", Age = 1, Breed = "Viralata", Color = "Preto", Gender = "M", Weight = 3.5 };
            var catId = 10;

            // Act
            var data = controller.Update(catId, catRequest);

            // Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact(Skip = "Para este teste passar, � preciso lan�ar uma exce��o no controller")]
        public void Update_Return_BadRequestResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catRequest = new UpdateCatRequest { Name = "Jonas atualizado", Age = 1, Breed = "Viralata", Color = "Preto", Gender = "M", Weight = 3.5 };
            var catId = 2;

            // Act
            var data = controller.Update(catId, catRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public void Update_MatchResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catRequest = new UpdateCatRequest { Name = "Joana atualizada", Age = 15, Breed = "Viralata", Color = "Marrom", Gender = "F", Weight = 3.5 };
            var catId = 3;

            // Act
            var data = controller.Update(catId, catRequest);

            // Assert
            Assert.IsType<NoContentResult>(data);

            var catUpdated = _unitOfWork.CatRepository.GetById(cat => cat.Id == catId);

            Assert.Equal(catId, catUpdated.Id);
            Assert.Equal(catRequest.Name, catUpdated.Name);
            Assert.Equal(catRequest.Breed, catUpdated.Breed);
            Assert.Equal(catRequest.Weight, catUpdated.Weight);
            Assert.Equal(catRequest.Age, catUpdated.Age);
            Assert.Equal(catRequest.Gender, catUpdated.Gender);
        }

        // ============================================ PartialUpdate action ========================================
        [Fact]
        public void PartialUpdate_Return_NoContentResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 2;
            JsonPatchDocument<UpdateCatRequest> patch = new JsonPatchDocument<UpdateCatRequest>();
            patch.Replace(cat => cat.Name, "Joana parcialmente atualizada");
            patch.Replace(cat => cat.Color, "Amarelo");

            // Act
            var data = controller.PartialUpdate(catId, patch);

            // Assert
            Assert.IsType<NoContentResult>(data);
        }

        // ============================================ Destroy action ==============================================
        [Fact]
        public void Destroy_Return_NoContentResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 3;

            // Act
            var data = controller.Destroy(catId);

            // Assert
            Assert.IsType<NoContentResult>(data);
        }

        [Fact]
        public void Destroy_Return_NotFoundResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 10;

            CatParameters catParameters = new CatParameters();
            catParameters.PageNumber = 1;
            catParameters.PageSize = 10;

            var cat = _unitOfWork.CatRepository.GetCatsVaccines(catParameters);
            // Act
            var data = controller.Destroy(catId);

            // Assert
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact(Skip = "Para este teste passar, � preciso lan�ar uma exce��o no controller")]
        public void Destroy_Return_BadRequestResult()
        {
            // Arrange
            var controller = new CatController(_unitOfWork, _mapper);
            var catId = 1;

            // Act
            var data = controller.Destroy(catId);

            // Assert
            Assert.IsType<BadRequestObjectResult>(data);
        }
    }
}