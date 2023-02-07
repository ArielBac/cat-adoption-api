using CatAdotionApi.Data;
using CatAdotionApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatAdotionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CatController
{
	private CatAdoptionContext _context;
	public CatController(CatAdoptionContext context)
	{
		_context = context;
	}

	[HttpGet]
	public IEnumerable<Cat> index()
	{
		return _context.Cats;
	}
}
