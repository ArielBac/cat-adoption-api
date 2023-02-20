using AutoMapper;
using CatAdoptionApi.Data;
using CatAdoptionApi.Data.Dtos.Cats;
using CatAdoptionApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Recupera todos os gatinhos cadastrados
    /// </summary>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Retorna todos os gatinhos cadastrados</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ReadCatDto>), StatusCodes.Status200OK)]
    public IEnumerable<ReadCatDto> Index(int skip = 0, int take = 10)
    {
        return _mapper.Map<List<ReadCatDto>>(_context.Cats
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .Include(vaccines => vaccines.Vaccines));
    }

    /// <summary>
    /// Cria um gatinho
    /// </summary>
    /// <param name="catDto"></param>
    /// <response code="201">Retorna o gatinho criado</response>           
    /// <response code="400">Erro no corpo da requisição</response>                      
    [HttpPost]
    [ProducesResponseType(typeof(Cat), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateCatDto catDto)
    {
        Cat cat = _mapper.Map<Cat>(catDto);
        _context.Cats.Add(cat);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Show), new { id = cat.Id }, cat); // Informa ao usuário em qual caminho ele pode encontrar o recurso criado
    }

    /// <summary>
    /// Retorna um gatinho
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Retorna o gatinho pesquisado</response>           
    /// <response code="404">Gatinho não encontrado</response>
    [ProducesResponseType(typeof(ReadCatDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        var cat = _context.Cats.AsNoTracking().Include(vaccines => vaccines.Vaccines).FirstOrDefault(cat => cat.Id == id);
        if (cat == null) return NotFound();
        var catDto = _mapper.Map<ReadCatDto>(cat);
        return Ok(catDto);
    }

    /// <summary>
    /// Atualiza um gatinho
    /// </summary>
    /// <param name="id"></param>
    /// <param name="catDto"></param>
    /// <response code="204">Gatinho atualizado com sucesso</response>           
    /// <response code="404">Gatinho não encontrado</response>
    /// <response code="400">Erro no corpo da requisição</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateCatDto catDto)
    {
        var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
        if (cat == null) return NotFound();
        _mapper.Map(catDto, cat);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza um gatinho parcialmente
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patch"></param>
    /// <response code="204">Gatinho atualizado com sucesso</response>           
    /// <response code="404">Gatinho não encontrado</response>
    /// <response code="400">Erro no corpo da requisição</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPatch("{id}")]
    public IActionResult PartialUpdate(int id, JsonPatchDocument<UpdateCatDto> patch)
    {
        var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
        if (cat == null) return NotFound();

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

    /// <summary>
    /// Remove um gatinho
    /// </summary>
    /// <param name="id"></param>
    /// <response code="204">Gatinho removido com sucesso</response>           
    /// <response code="404">Gatinho não encontrado</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public IActionResult Destroy(int id)
    {
        var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
        if (cat == null) return NotFound();
        _context.Cats.Remove(cat);
        _context.SaveChanges();
        return NoContent();
    }
}
