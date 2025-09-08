using ContractManager.Application.Features.Auth.Commands.Login;
using ContractManager.Application.Features.Auth.Commands.Register;
using ContractManager.Application.Features.Users.Queries.GetUserList; // AJOUT
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContractManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { Token = token });
    }

    // AJOUT de la nouvelle méthode pour lister les utilisateurs
    [HttpGet]
    [Authorize(Roles = "Admin")] // Seuls les admins peuvent voir la liste
    public async Task<IActionResult> GetUsers()
    {
        var users = await _mediator.Send(new GetUserListQuery());
        return Ok(users);
    }
}