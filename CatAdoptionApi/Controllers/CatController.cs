using AutoMapper;
using CatAdoptionApi.Data;
using CatAdoptionApi.Data.Dtos;
using CatAdoptionApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CatAdoptionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatController : ControllerBase
{
	private CatAdoptionContext _context;
	private IMapper _mapper;
    public CatController(CatAdoptionContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
	}

	[HttpGet]
	public IEnumerable<ReadCatDto> Index()
	{
		return _mapper.Map<List<ReadCatDto>>(_context.Cats);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public IActionResult Create([FromBody] UpdateCatDto catDto)
	{
		Cat cat = _mapper.Map<Cat>(catDto);
		_context.Cats.Add(cat);
		_context.SaveChanges();
		return CreatedAtAction(nameof(Show), new { id = cat.Id }, cat); // Informa ao usuário em qual caminho ele pode encontrar o recurso criado
	}

	[HttpGet("{id}")]
	public IActionResult Show(int id)
	{
		var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
		if (cat == null) return NotFound();
		var catDto = _mapper.Map<ReadCatDto>(cat);
		return Ok(catDto);
	}

	// TODO
	// Implementar DTOs e utilizar AutoMapper para melhorar esse procedimento
	[HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateCatDto catDto)
    {
        var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
		if (cat == null) return NotFound();
		_mapper.Map(catDto, cat);
		_context.SaveChanges();
		return NoContent();
    }

	[HttpPatch("{id}")]
	public IActionResult ParcialUpdate(int id, JsonPatchDocument<UpdateCatDto> patch)
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
