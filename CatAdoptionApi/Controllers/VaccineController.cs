using AutoMapper;
using CatAdoptionApi.Data;
using CatAdoptionApi.Data.Dtos.Cats;
using CatAdoptionApi.Data.Dtos.Vaccines;
using CatAdoptionApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Recupera todas as vacinas que já foram aplicadas
    /// </summary>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Retorna todos os gatinhos cadastrados</response>
    [HttpGet]
    [ProducesResponseType(typeof(ICollection<ReadVaccineDto>), StatusCodes.Status200OK)]
    public ICollection<ReadVaccineDto> Index()
    {
        return _mapper.Map<List<ReadVaccineDto>>(_context.Vaccines.Include(cat => cat.Cat));
    }

    /// <summary>
    /// Cria um registro de vacina aplicada a um gatinho
    /// </summary>
    /// <param name="vaccineDto"></param>
    /// <response code="201">Retorna a vacina criada</response>           
    /// <response code="400">Erro no corpo da requisição</response>                      
    [HttpPost]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateVaccineDto vaccineDto)
    {
        Vaccine vaccine = _mapper.Map<Vaccine>(vaccineDto);
        _context.Vaccines.Add(vaccine);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Show), new { id = vaccine.Id }, vaccine);
    }


    /// <summary>
    /// Retorna uma vacina com o gatinho que recebeu ela
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna a vacina pesquisado</response>           
    /// <response code="404">Vacina não encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Show(int id)
    {
        var vaccine = _context.Vaccines.Include(cat => cat.Cat).FirstOrDefault(vaccine => vaccine.Id == id);
        if (vaccine == null) return NotFound();
        var vaccineDto = _mapper.Map<ReadVaccineDto>(vaccine);
        return Ok(vaccineDto);
    }

    /// <summary>
    /// Atualiza uma vacina
    /// </summary>
    /// <param name="id"></param>
    /// <param name="vaccineDto"></param>
    /// <response code="204">Vacina atualizada com sucesso</response>           
    /// <response code="404">Vacina não encontrado</response>
    /// <response code="400">Erro no corpo da requisição</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateVaccineDto vaccineDto)
    {
        var vaccine = _context.Vaccines.FirstOrDefault(vaccine => vaccine.Id == id);
        if (vaccine == null) return NotFound();
        _mapper.Map(vaccineDto, vaccine);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza uma vacina parcialmente
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patch"></param>
    /// <response code="204">Vacina atualizada com sucesso</response>           
    /// <response code="404">Vacina não encontrado</response>
    /// <response code="400">Erro no corpo da requisição</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPatch("{id}")]
    public IActionResult PartialUpdate(int id, [FromBody] JsonPatchDocument<UpdateVaccineDto> patch)
    {
        var vaccine = _context.Vaccines.FirstOrDefault(vaccine => vaccine.Id == id);
        if (vaccine == null) return NotFound();

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
}
