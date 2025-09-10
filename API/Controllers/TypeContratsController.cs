using ContractManager.Application.Features.TypeContrats.Commands.CreateTypeContrat;
using ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratDetail;
using ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratList;
using ContractManager.Application.Features.TypeContrats.Queries.GetChampsObligatoires;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TypeContratsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TypeContratsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool? estActif = null)
    {
        var query = new GetTypeContratListQuery { EstActif = estActif };
        var typeContrats = await _mediator.Send(query);
        return Ok(typeContrats);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var query = new GetTypeContratDetailQuery { Id = id };
        var typeContrat = await _mediator.Send(query);
        return Ok(typeContrat);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, RH")]
    public async Task<IActionResult> Create([FromBody] CreateTypeContratCommand command)
    {
        var typeContratId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = typeContratId }, command);
    }

    [HttpGet("{typeContratId}/champs-obligatoires")]
    public async Task<IActionResult> GetChampsObligatoires(int typeContratId, [FromQuery] int? paysId = null)
    {
        var query = new GetChampsObligatoiresQuery { TypeContratId = typeContratId, PaysId = paysId };
        var champs = await _mediator.Send(query);
        return Ok(champs);
    }
}
