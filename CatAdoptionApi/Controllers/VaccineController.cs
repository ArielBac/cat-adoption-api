using AutoMapper;
using CatAdoptionApi.Data;
using CatAdoptionApi.Data.Dtos.Vaccines;
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
    /// Recupera todos as vacinas cadastrados
    /// </summary>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Retorna todos os gatinhos cadastrados</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<ReadVaccineDto>), StatusCodes.Status200OK)]
    public ICollection<ReadVaccineDto> Index()
    {
        return _mapper.Map<List<ReadVaccineDto>>(_context.Vaccines.Include(cat => cat.Cat));
    }
}
