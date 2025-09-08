using ContractManager.Application.Features.Avenants.Commands.CreateAvenant;
using ContractManager.Application.Features.Avenants.Queries.GetAvenantsForContrat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/contrats/{contratId}/avenants")] // Route imbriquée pour lier les avenants à un contrat
[Authorize]
public class AvenantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AvenantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int contratId)
    {
        var query = new GetAvenantsForContratQuery { ContratId = contratId };
        var avenants = await _mediator.Send(query);
        return Ok(avenants);
    }

    [HttpPost]
    public async Task<IActionResult> Post(int contratId, [FromBody] CreateAvenantCommand command)
    {
        // On s'assure que l'avenant est bien créé pour le bon contrat
        command.ContratId = contratId;
        var avenantId = await _mediator.Send(command);
        return Ok(avenantId);
    }
}
