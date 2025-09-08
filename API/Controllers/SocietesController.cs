using ContractManager.Application.Features.Societes.Commands.CreateSociete;
using ContractManager.Application.Features.Societes.Queries.GetSocieteList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // On protège ce contrôleur, seules les personnes connectées peuvent y accéder.
public class SocietesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SocietesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var societes = await _mediator.Send(new GetSocieteListQuery());
        return Ok(societes);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSocieteCommand command)
    {
        var societeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = societeId }, command);
    }
}
