using AutoMapper;
using CatAdoptionApi.Data;
using CatAdoptionApi.Data.Dtos.Cats;
using CatAdoptionApi.Models;
using CatAdoptionApi.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CatAdoptionApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class CatController : ControllerBase
{
    private CatAdoptionContext _context;
    private IMapper _mapper;

    public CatController(CatAdoptionContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [SwaggerOperation(
        Summary = "Recupera todos os gatinhos cadastrados",
        Description = "Retorna todos os registros da tabela de gatos do banco de dados"
    )]
    [SwaggerResponse(200, "Lista de gatinhos retornada com sucesso", typeof(IEnumerable<ReadCatDto>))]
    [HttpGet]
    public IEnumerable<ReadCatDto> Index(
        [FromQuery, SwaggerParameter("Número de registros pulados", Required = false)] int skip = 0, 
        [FromQuery, SwaggerParameter("Número de registros retornados", Required = false)] int take = 10
    )
    {
        try
        {
            return _mapper.Map<List<ReadCatDto>>(_context.Cats
                            .AsNoTracking()
                            .Skip(skip)
                            .Take(take)
                            .Include(vaccines => vaccines.Vaccines));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [SwaggerOperation(
        Summary = "Cadastra um gatinho",
        Description = "Insere um registro na tabela de gatos no banco de dados"
    )]
    [SwaggerResponse(201, "Gatinho cadastrado com sucesso", typeof(ReadCatDto))]
    [SwaggerResponse(400, "Erro na requisição")]
    [HttpPost]
    public IActionResult Create(
        [FromBody, SwaggerParameter("Dados para o cadastro de um gatinho", Required = true)] CreateCatDto catDto
    )
    {
        try
        {
            Cat cat = _mapper.Map<Cat>(catDto);

            _context.Cats.Add(cat);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Show), new { id = cat.Id }, cat); // Informa ao usuário em qual caminho ele pode encontrar o recurso criado
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }

    [SwaggerOperation(
        Summary = "Recupera um gatinho",
        Description = "Retorna um registro da tabela de gatos no banco de dados, por id"
    )]
    [SwaggerResponse(200, "Gatinho retornado com sucesso", typeof(ReadCatDto))]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Gatinho não encontrado")]
    [HttpGet("{id:int}")]
    public IActionResult Show(
        [SwaggerParameter("Id do gatinhos a ser retornado", Required  = true)] int id
    )
    {
        try
        {
            var cat = _context.Cats
                .AsNoTracking()
                .Include(vaccines => vaccines.Vaccines)
                .FirstOrDefault(cat => cat.Id == id);

            if (cat == null) 
                return NotFound();

            var catDto = _mapper.Map<ReadCatDto>(cat);

            return Ok(catDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
    public IActionResult Update(
        [SwaggerParameter("Id do gatinho a ser atualizado", Required = true)] int id, 
        [FromBody, SwaggerParameter("Dados para a atualização de um gatinho", Required = true)] UpdateCatDto catDto
    )
    {
        try
        {
            var cat = _context.Cats
                .FirstOrDefault(cat => cat.Id == id);

            if (cat == null) 
                return NotFound();

            _mapper.Map(catDto, cat);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
        [FromBody, SwaggerParameter("Dados para a atualização de um gatinho")] JsonPatchDocument<UpdateCatDto> patch)
    {
        try
        {
            var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
            if (cat == null) 
                return NotFound();

            // Verificar se os campos de patch são válidos
            var catToUpdate = _mapper.Map<UpdateCatDto>(cat);
            patch.ApplyTo(catToUpdate, ModelState);

            if (!TryValidateModel(catToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(catToUpdate, cat);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
    public IActionResult Destroy(
        [SwaggerParameter("Id do gatinho a ser removido", Required = true)] int id
    )
    {
        try
        {
            var cat = _context.Cats
                .FirstOrDefault(cat => cat.Id == id);
            
            if (cat == null) 
                return NotFound();
            
            _context.Cats.Remove(cat);
            _context.SaveChanges();
            
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}
