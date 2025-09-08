using ContractManager.Application.Features.Employes.Commands.CreateEmploye;
using ContractManager.Application.Features.Employes.Queries.GetEmployeList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // L'accès au contrôleur nécessite d'être connecté
public class EmployesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var employes = await _mediator.Send(new GetEmployeListQuery());
        return Ok(employes);
    }

    // CORRECTION : On autorise maintenant les Admins, RH et Managers à créer des employés.
    [HttpPost]
    [Authorize(Roles = "Admin, RH, Manager")]
    public async Task<IActionResult> Post([FromBody] CreateEmployeCommand command)
    {
        var employeId = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = employeId }, command);
    }
}
