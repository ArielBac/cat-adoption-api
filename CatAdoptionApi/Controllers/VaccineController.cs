using AutoMapper;
using CatAdoptionApi.Models;
using CatAdoptionApi.Pagination;
using CatAdoptionApi.Repository;
using CatAdoptionApi.Requests.Cats;
using CatAdoptionApi.Requests.Vaccines;
using CatAdoptionApi.SwaggerExamples.Cats;
using CatAdoptionApi.SwaggerExamples.Vaccines;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json;

namespace CatAdoptionApi.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class VaccineController : ControllerBase
{
    private IMapper _mapper;
    private IUnitOfWork _unitOfWork;

    public VaccineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [SwaggerOperation(
       Summary = "Recupera todas as vacinas cadastrados",
       Description = "Retorna todos os registros da tabela de vacinas do banco de dados"
    )]
    [SwaggerResponse(200, "Lista de vacinas retornada com sucesso", typeof(IEnumerable<GetVaccineRequest>))]
    [SwaggerResponseExample(200, typeof(GetVaccineResponseExample))]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetVaccineRequest>>> Index(
        [FromQuery, SwaggerParameter("Número de registros pulados", Required = false)] VaccineParameters vaccineParameters
    )
    {
        try
        {
            var vaccines = await _unitOfWork.VaccineRepository.GetVaccinesCat(vaccineParameters);

            var metadata = new
            {
                vaccines.TotalCount,
                vaccines.PageSize,
                vaccines.CurrentPage,
                vaccines.TotalPages,
                vaccines.HasNext,
                vaccines.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            var vaccinesGetRequest = _mapper.Map<List<GetVaccineRequest>>(vaccines);

            return vaccinesGetRequest;
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
    }

    [SwaggerOperation(
       Summary = "Cadastra uma vacina aplicada a um gatinho",
       Description = "Insere um registro na tabela de vacinas no banco de dados"
    )]
    [SwaggerResponse(201, "Vacina cadastrada com sucesso", typeof(GetVaccineRequest))]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerRequestExample(typeof(CreateCatRequest), typeof(CreateVaccineRequestExample))]
    [SwaggerResponseExample(201, typeof(CreateVaccineResponseExample))]
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody, SwaggerParameter("Dados para o cadastro de uma vacina", Required = true)] CreateVaccineRequest vaccineRequest
    )
    {
        try
        {
            var vaccine = _mapper.Map<Vaccine>(vaccineRequest);

            _unitOfWork.VaccineRepository.Add(vaccine);
            await _unitOfWork.Commit();

            var vaccineGetRequest = _mapper.Map<GetVaccineRequest>(vaccine);

            return CreatedAtAction(nameof(Show), new { id = vaccine.Id }, vaccineGetRequest);
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
    }

    [SwaggerOperation(
        Summary = "Recupera uma vacina",
        Description = "Retorna um registro da tabela de vacinas no banco de dados, por id"
    )]
    [SwaggerResponse(200, "Vacina retornada com sucesso", typeof(GetVaccineRequest))]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [SwaggerResponseExample(200, typeof(GetVaccineByIdResponseExample))]
    [HttpGet("{id}")]
    public async Task<ActionResult<GetVaccineRequest>> Show(
        [SwaggerParameter("Id da vacina a ser retornada", Required = true)] int id
    )
    {
        try
        {
            var vaccine = await _unitOfWork.VaccineRepository.GetVaccineCat(vaccine => vaccine.Id == id);
            
            if (vaccine == null) 
                return NotFound();

            var vaccineGetRequest = _mapper.Map<GetVaccineRequest>(vaccine);
           
            return vaccineGetRequest;
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
    }

    [SwaggerOperation(
        Summary = "Atualiza uma vacina",
        Description = "Atualiza um registro da tabela de vacinas no banco de dados, por id"
    )]
    [SwaggerResponse(204, "Vacina atualizada com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [SwaggerRequestExample(typeof(UpdateVaccineRequest), typeof(UpdateVaccineRequestExample))]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(
        [SwaggerParameter("Id da vacina a ser atualizada", Required = true)] int id,
        [FromBody, SwaggerParameter("Dados para a atualização de uma vacina", Required = true)] UpdateVaccineRequest vaccineRequest
    )
    {
        try
        {
            var vaccine = await _unitOfWork.VaccineRepository.GetById(vaccine => vaccine.Id == id);

            if (vaccine == null) 
                return NotFound();

            _mapper.Map(vaccineRequest, vaccine);
            _unitOfWork.VaccineRepository.Update(vaccine);
            await _unitOfWork.Commit();

            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
    }

    [SwaggerOperation(
      Summary = "Atualiza uma vacina parcialmente",
      Description = "Atualiza um registro da tabela de vacinas no banco de dados, parcialmente, por id"
    )]
    [SwaggerResponse(204, "Vacina atualizada com sucesso")]
    [SwaggerResponse(400, "Erro na requisição")]
    [SwaggerResponse(404, "Vacina não encontrada")]
    [SwaggerRequestExample(typeof(JsonPatchDocument<UpdateVaccineRequest>), typeof(PartialUpdateVaccineRequestExample))]
    [HttpPatch("{id}")]
    public async Task<IActionResult> PartialUpdate(
        [SwaggerParameter("Id da vacina a ser atualizada", Required = true)] int id,
        [FromBody, SwaggerParameter("Dados para a atualização de uma vacina")] JsonPatchDocument<UpdateVaccineRequest> patch
    )
    {
        try
        {
            var vaccine = await _unitOfWork.VaccineRepository.GetById(vaccine => vaccine.Id == id);

            if (vaccine == null) 
                return NotFound();

            var vaccineToUpdate = _mapper.Map<UpdateVaccineRequest>(vaccine);
            patch.ApplyTo(vaccineToUpdate, ModelState);

            _mapper.Map(vaccineToUpdate, vaccine);
            _unitOfWork.VaccineRepository.Update(vaccine);
            await _unitOfWork.Commit();

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
    [HttpDelete("{id}")]
    public async Task<ActionResult> Destroy(
        [SwaggerParameter("Id da vacina a ser removida", Required = true)] int id
    ) 
    {
        try
        {
            var vaccine = await _unitOfWork.VaccineRepository.GetById(vaccine => vaccine.Id == id);

            if (vaccine == null) 
                return NotFound();
            
            _unitOfWork.VaccineRepository.Delete(vaccine);
            await _unitOfWork.Commit();

            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest("Um erro inesperado ocorreu");
        }
       
    }
}
