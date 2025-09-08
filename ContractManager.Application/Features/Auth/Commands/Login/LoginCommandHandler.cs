using ContractManager.Application.Contracts.Infrastructure;
using ContractManager.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging; // 1. Ajoutez ce using
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<LoginCommandHandler> _logger; // 2. Injectez le logger

    public LoginCommandHandler(
        IUtilisateurRepository utilisateurRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<LoginCommandHandler> logger)
    {
        _utilisateurRepository = utilisateurRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Tentative de connexion pour l'email : {Email}", request.Email);

        var utilisateur = await _utilisateurRepository.GetByEmailAsync(request.Email);
        if (utilisateur is null)
        {
            _logger.LogWarning("Échec de la connexion : utilisateur non trouvé pour l'email {Email}", request.Email);
            throw new Exception("Email ou mot de passe invalide.");
        }

        _logger.LogInformation("Utilisateur trouvé pour {Email}. Hash stocké : {Hash}", request.Email, utilisateur.MotDePasseHash);

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, utilisateur.MotDePasseHash);
        if (!isPasswordValid)
        {
            _logger.LogWarning("Échec de la connexion : mot de passe invalide pour {Email}", request.Email);
            throw new Exception("Email ou mot de passe invalide.");
        }

        _logger.LogInformation("Connexion réussie pour {Email}", request.Email);
        var token = _jwtTokenGenerator.GenerateToken(utilisateur);
        return token;
    }
}
