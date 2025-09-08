using ContractManager.Application.Features.Contrats.Commands.ArchiveContrat;
using ContractManager.Application.Features.Contrats.Commands.CreateContrat;
using ContractManager.Application.Features.Contrats.Queries.GetContractsForValidation;
using ContractManager.Application.Features.Contrats.Queries.GetContratDetail;
using ContractManager.Application.Features.Contrats.Queries.GetContratList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContratsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContratsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var contrat = await _mediator.Send(new GetContratDetailQuery { Id = id });
        return Ok(contrat);
    }

    [HttpGet]
    [Authorize(Roles = "RH, Admin")]
    public async Task<IActionResult> GetAll()
    {
        var contrats = await _mediator.Send(new GetContratListQuery());
        return Ok(contrats);
    }

    [HttpGet("validation")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetForValidation()
    {
        var contrats = await _mediator.Send(new GetContractsForValidationQuery());
        return Ok(contrats);
    }

    [HttpPost]
    [Authorize(Roles = "RH, Admin")]
    public async Task<IActionResult> Post([FromBody] CreateContratCommand command)
    {
        var contratId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = contratId }, command);
    }
    [HttpPost("{id}/archive")]
    [Authorize(Roles = "Admin, RH")]
    public async Task<IActionResult> Archive(int id)
    {
        var command = new ArchiveContratCommand { ContratId = id };
        await _mediator.Send(command);
        return NoContent(); // 204 No Content est une bonne réponse pour une action réussie.
    }

}
