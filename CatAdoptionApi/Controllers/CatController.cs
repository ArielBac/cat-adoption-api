using AutoMapper;
using CatAdoptionApi.Models;
using CatAdoptionApi.Repository;
using CatAdoptionApi.Requests.Cats;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CatAdoptionApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CatController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public CatController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [SwaggerOperation(
        Summary = "Recupera todos os gatinhos cadastrados",
        Description = "Retorna todos os registros da tabela de gatos do banco de dados"
    )]
    [SwaggerResponse(200, "Lista de gatinhos retornada com sucesso", typeof(IEnumerable<GetCatRequest>))]
    [SwaggerResponse(400, "Erro inesperado")]
    [HttpGet]
    public ActionResult<IEnumerable<GetCatRequest>> Index(
        [FromQuery, SwaggerParameter("Número de registros pulados", Required = false)] int skip = 0, 
        [FromQuery, SwaggerParameter("Número de registros retornados", Required = false)] int take = 10
    )
    {
        try
        {
            var cats = _unitOfWork.CatRepository
                            .GetCatsVaccines()
                            .Skip(skip)
                            .Take(take);
            
            var catsGetRequest = _mapper.Map<List<GetCatRequest>>(cats);
            
            return catsGetRequest;
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
    }

    [SwaggerOperation(
        Summary = "Cadastra um gatinho",
        Description = "Insere um registro na tabela de gatos no banco de dados"
    )]
    [SwaggerResponse(201, "Gatinho cadastrado com sucesso", typeof(GetCatRequest))]
    [SwaggerResponse(400, "Erro na requisição")]
    [HttpPost]
    public ActionResult Create(
        [FromBody, SwaggerParameter("Dados para o cadastro de um gatinho", Required = true)] CreateCatRequest catRequest
    )
    {
        try
        {
            var cat = _mapper.Map<Cat>(catRequest);

            _unitOfWork.CatRepository.Add(cat);
            _unitOfWork.Commit();

            var catGetRequest = _mapper.Map<GetCatRequest>(cat);

            return CreatedAtAction(nameof(Show), new { id = cat.Id }, catGetRequest); // Informa ao usuário em qual caminho ele pode encontrar o recurso criado
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
       
    }

    [SwaggerOperation(
        Summary = "Recupera um gatinho",
        Description = "Retorna um registro da tabela de gatos no banco de dados, por id"
    )]
    [SwaggerResponse(200, "Gatinho retornado com sucesso", typeof(GetCatRequest))]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Gatinho não encontrado")]
    [HttpGet("{id:int}")]
    public ActionResult<GetCatRequest> Show(
        [SwaggerParameter("Id do gatinhos a ser retornado", Required  = true)] int id
    )
    {
        try
        {
            var cat = _unitOfWork.CatRepository.GetCatVaccines(c => c.Id == id);

            if (cat == null)
                return NotFound();

            var catGetRequest = _mapper.Map<GetCatRequest>(cat);

            return catGetRequest;
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
       
    }

    [SwaggerOperation(
        Summary = "Atualiza um gatinho",
        Description = "Atualiza um registro da tabela de gatos no banco de dados, por id"
    )]
    [SwaggerResponse(204, "Gatinho atualizado com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Gatinho não encontrado")]
    [HttpPut("{id:int}")]
    public ActionResult Update(
        [SwaggerParameter("Id do gatinho a ser atualizado", Required = true)] int id, 
        [FromBody, SwaggerParameter("Dados para a atualização de um gatinho", Required = true)] UpdateCatRequest catRequest
    )
    {
        try
        {
            var cat = _unitOfWork.CatRepository.GetById(cat => cat.Id == id);

            if (cat == null) 
                return NotFound();

            _mapper.Map(catRequest, cat);
            _unitOfWork.CatRepository.Update(cat);
            _unitOfWork.Commit();

            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
       
    }

    [SwaggerOperation(
       Summary = "Atualiza um gatinho parcialmente",
       Description = "Atualiza um registro da tabela de gatos no banco de dados, parcialmente, por id"
    )]
    [SwaggerResponse(204, "Gatinho atualizado com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Gatinho não encontrado")]
    [HttpPatch("{id:int}")]
    public IActionResult PartialUpdate(
        [SwaggerParameter("Id do gatinho a ser atualizado", Required = true)] int id, 
        [FromBody, SwaggerParameter("Dados para a atualização de um gatinho")] JsonPatchDocument<UpdateCatRequest> patch)
    {
        try
        {
            var cat = _unitOfWork.CatRepository.GetById(cat => cat.Id == id);
            if (cat == null)
                return NotFound();

            // Verificar se os campos de patch são válidos
            var catToUpdate = _mapper.Map<UpdateCatRequest>(cat);
            patch.ApplyTo(catToUpdate, ModelState);

            _mapper.Map(catToUpdate, cat);
            _unitOfWork.CatRepository.Update(cat);
            _unitOfWork.Commit();

            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
        
    }

    [SwaggerOperation(
       Summary = "Remove um gatinho",
       Description = "Remove um registro da tabela de gatos no banco de dados, por id"
    )]
    [SwaggerResponse(204, "Gatinho removido com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Gatinho não encontrado")]
    [HttpDelete("{id:int}")]
    public ActionResult Destroy(
        [SwaggerParameter("Id do gatinho a ser removido", Required = true)] int id
    )
    {
        try
        {
            var cat = _unitOfWork.CatRepository.GetById(cat => cat.Id == id);
            
            if (cat == null) 
                return NotFound();
            
            _unitOfWork.CatRepository.Delete(cat);
            _unitOfWork.Commit();
            
            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
        
    }
}
