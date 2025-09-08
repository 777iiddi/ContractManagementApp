using ContractManager.Application.Contracts.Infrastructure;
using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<RegisterCommandHandler> _logger; 
    public RegisterCommandHandler(
        IUtilisateurRepository utilisateurRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogger<RegisterCommandHandler> logger)
    {
        _utilisateurRepository = utilisateurRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Début de l'inscription pour l'email : {Email}", request.Email);

        if (await _utilisateurRepository.GetByEmailAsync(request.Email) is not null)
        {
            _logger.LogWarning("Échec de l'inscription : l'email {Email} existe déjà.", request.Email);
            throw new Exception("Un utilisateur avec cet email existe déjà.");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _logger.LogInformation("Le mot de passe pour {Email} a été haché.", request.Email);

        var utilisateur = new Utilisateur
        {
            Email = request.Email,
            MotDePasseHash = passwordHash,
            Role = request.Role
        };

        var newUser = await _utilisateurRepository.AddAsync(utilisateur, cancellationToken);
        _logger.LogInformation("L'utilisateur {Email} a été sauvegardé avec l'ID {Id}.", newUser.Email, newUser.Id);

        var token = _jwtTokenGenerator.GenerateToken(newUser);
        return token;
    }
}
