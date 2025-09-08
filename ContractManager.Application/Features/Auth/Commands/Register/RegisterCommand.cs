using MediatR;

namespace ContractManager.Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    // Le rôle sera maintenant fourni par l'administrateur lors de la création.
    public string Role { get; set; } = string.Empty;
}
