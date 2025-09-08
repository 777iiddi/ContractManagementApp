using ContractManager.Application.Features.Workflows.Commands.RejectEtape;
using ContractManager.Application.Features.Workflows.Commands.ValidateEtape;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WorkflowsController : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkflowsController(IMediator mediator) => _mediator = mediator;

    [HttpPost("etapes/{etapeId}/valider")]
    public async Task<IActionResult> ValidateEtape(int etapeId, [FromBody] string commentaire)
    {
        var command = new ValidateEtapeCommand { EtapeWorkflowId = etapeId, Commentaire = commentaire };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPost("etapes/{etapeId}/rejeter")]
    public async Task<IActionResult> RejectEtape(int etapeId, [FromBody] string commentaire)
    {
        var command = new RejectEtapeCommand { EtapeWorkflowId = etapeId, Commentaire = commentaire };
        await _mediator.Send(command);
        return NoContent();
    }
}
