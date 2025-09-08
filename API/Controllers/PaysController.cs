using ContractManager.Application.Features.Pays.Commands.CreatePays;
using ContractManager.Application.Features.Pays.Queries.GetPaysList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; 


namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class PaysController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var pays = await _mediator.Send(new GetPaysListQuery());
        return Ok(pays);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePaysCommand command)
    {
        var paysId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = paysId }, command);
    }
}
