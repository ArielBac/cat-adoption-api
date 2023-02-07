using CatAdotionApi.Data;
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
}
