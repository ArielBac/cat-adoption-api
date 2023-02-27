using AutoMapper;
using CatAdoptionApi.Data;
using CatAdoptionApi.Data.Dtos.Cats;
using CatAdoptionApi.Data.Dtos.Vaccines;
using CatAdoptionApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace CatAdoptionApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VaccineController : ControllerBase
{
    private CatAdoptionContext _context;
    private IMapper _mapper;

    public VaccineController(CatAdoptionContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [SwaggerOperation(
       Summary = "Recupera todas as vacinas cadastrados",
       Description = "Retorna todos os registros da tabela de vacinas do banco de dados"
    )]
    [SwaggerResponse(200, "Lista de vacinas retornada com sucesso", typeof(IEnumerable<ReadVaccineDto>))]
    [HttpGet]
    public ICollection<ReadVaccineDto> Index(
        [FromQuery, SwaggerParameter("Número de registros pulados", Required = false)] int skip = 0,
        [FromQuery, SwaggerParameter("Número de registros retornados", Required = false)] int take = 10
    )
    {
        try
        {
            return _mapper.Map<List<ReadVaccineDto>>(_context.Vaccines
                           .AsNoTracking()
                           .Skip(skip)
                           .Take(take)
                           .Include(cat => cat.Cat));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [SwaggerOperation(
       Summary = "Cadastra uma vacina aplicada a um gatinho",
       Description = "Insere um registro na tabela de vacinas no banco de dados"
    )]
    [SwaggerResponse(201, "Vacina cadastrada com sucesso", typeof(ReadVaccineDto))]
    [SwaggerResponse(400, "Erro na requisição")]           
    [HttpPost]
    public IActionResult Create(
        [FromBody, SwaggerParameter("Dados para o cadastro de uma vacina", Required = true)] CreateVaccineDto vaccineDto
    )
    {
        try
        {
            Vaccine vaccine = _mapper.Map<Vaccine>(vaccineDto);

            _context.Vaccines.Add(vaccine);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Show), new { id = vaccine.Id }, vaccine);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [SwaggerOperation(
        Summary = "Recupera uma vacina",
        Description = "Retorna um registro da tabela de vacinas no banco de dados, por id"
    )]
    [SwaggerResponse(200, "Vacina retornada com sucesso", typeof(ReadVaccineDto))]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [HttpGet("{id:int}")]
    public IActionResult Show(
        [SwaggerParameter("Id da vacina a ser retornada", Required = true)] int id
    )
    {
        try
        {
            var vaccine = _context.Vaccines
                .AsNoTracking()
                .Include(cat => cat.Cat)
                .FirstOrDefault(vaccine => vaccine.Id == id);
            
            if (vaccine == null) 
                return NotFound();

            var vaccineDto = _mapper.Map<ReadVaccineDto>(vaccine);
           
            return Ok(vaccineDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [SwaggerOperation(
        Summary = "Atualiza uma vacina",
        Description = "Atualiza um registro da tabela de vacinas no banco de dados, por id"
    )]
    [SwaggerResponse(204, "Vacina atualizada com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [HttpPut("{id:int}")]
    public IActionResult Update(
        [SwaggerParameter("Id da vacina a ser atualizada", Required = true)] int id,
        [FromBody, SwaggerParameter("Dados para a atualização de uma vacina", Required = true)] UpdateVaccineDto vaccineDto
    )
    {
        try
        {
            var vaccine = _context.Vaccines
                .FirstOrDefault(vaccine => vaccine.Id == id);

            if (vaccine == null) 
                return NotFound();

            _mapper.Map(vaccineDto, vaccine);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [SwaggerOperation(
      Summary = "Atualiza uma vacina parcialmente",
      Description = "Atualiza um registro da tabela de vacinas no banco de dados, parcialmente, por id"
    )]
    [SwaggerResponse(204, "Vacina atualizada com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [HttpPatch("{id:int}")]
    public IActionResult PartialUpdate(
        [SwaggerParameter("Id da vacina a ser atualizada", Required = true)] int id,
        [FromBody, SwaggerParameter("Dados para a atualização de uma vacina")] JsonPatchDocument<UpdateVaccineDto> patch
    )
    {
        try
        {
            var vaccine = _context.Vaccines
                .FirstOrDefault(vaccine => vaccine.Id == id);

            if (vaccine == null) 
                return NotFound();

            var vaccineToUpdate = _mapper.Map<UpdateVaccineDto>(vaccine);
            patch.ApplyTo(vaccineToUpdate, ModelState);

            if (!TryValidateModel(vaccineToUpdate))
            {
                return ValidationProblem();
            }

            _mapper.Map(vaccineToUpdate, vaccine);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }

    [SwaggerOperation(
       Summary = "Remove uma vacina",
       Description = "Remove um registro da tabela de vacinas no banco de dados, por id"
    )]
    [SwaggerResponse(204, "Vacina removida com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [HttpDelete("{id:int}")]
    public IActionResult Destroy(
        [SwaggerParameter("Id da vacina a ser removida", Required = true)] int id
    ) 
    {
        try
        {
            var vaccine = _context.Vaccines
                .FirstOrDefault(vaccine => vaccine.Id == id);

            if (vaccine == null) 
                return NotFound();
            
            _context.Vaccines.Remove(vaccine);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
       
    }
}
