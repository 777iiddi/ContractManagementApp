using ContractManager.Application.Features.TypeContrats.Commands.CreateTypeContrat;
using ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratList;
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
    public async Task<IActionResult> Get()
    {
        var typeContrats = await _mediator.Send(new GetTypeContratListQuery());
        return Ok(typeContrats);
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTypeContratCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(new { id });
    }
}
