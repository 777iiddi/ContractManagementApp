using MediatR;

namespace ContractManager.Application.Features.Auth.Commands.Login;

// La commande qui contient les données pour la connexion.
public class LoginCommand : IRequest<string> // Retourne un token JWT
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
