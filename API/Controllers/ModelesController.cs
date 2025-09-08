using ContractManager.Application.Features.Modeles.Commands.CreateOrUpdateModele;
using ContractManager.Application.Features.Modeles.Queries.GetModeleList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin, RH")]
public class ModelesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ModelesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var modeles = await _mediator.Send(new GetModeleListQuery());
        return Ok(modeles);
    }

    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateOrUpdateModeleCommand command)
    {
        var modeleId = await _mediator.Send(command);
        return Ok(modeleId);
    }
}
