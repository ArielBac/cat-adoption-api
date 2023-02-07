using CatAdotionApi.Data;
using CatAdotionApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatAdotionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatController : ControllerBase
{
	private CatAdoptionContext _context;
	public CatController(CatAdoptionContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IEnumerable<Cat> Index()
	{
		return _context.Cats;
	}

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] Cat cat)
	{
		_context.Cats.Add(cat);
		_context.SaveChanges();
        return CreatedAtAction(nameof(Show), new { id = cat.Id }, cat); // Vai informar pro usuário em qual caminho ele pode encontrar o resurso criado
    }

	[HttpGet("{id}")]
	public IActionResult Show(int id)
	{
		var cat = _context.Cats.FirstOrDefault(cat => cat.Id == id);
		if (cat == null) return NotFound();
		return Ok(cat);
	}
}
