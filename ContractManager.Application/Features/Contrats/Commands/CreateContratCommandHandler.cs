using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Commands.CreateContrat;

public class CreateContratCommandHandler : IRequestHandler<CreateContratCommand, int>
{
    private readonly IContratRepository _contratRepository;
    private readonly IEmployeRepository _employeRepository;
    private readonly ITypeContratRepository _typeContratRepository;
    private readonly ISocieteRepository _societeRepository;

    public CreateContratCommandHandler(
        IContratRepository contratRepository,
        IEmployeRepository employeRepository,
        ITypeContratRepository typeContratRepository,
        ISocieteRepository societeRepository)
    {
        _contratRepository = contratRepository;
        _employeRepository = employeRepository;
        _typeContratRepository = typeContratRepository;
        _societeRepository = societeRepository;
    }

    public async Task<int> Handle(CreateContratCommand request, CancellationToken cancellationToken)
    {
        // --- VÉRIFICATION (Votre solution) ---
        var employe = await _employeRepository.GetByIdAsync(request.EmployeId);
        if (employe == null)
        {
            throw new Exception($"L'employé avec l'ID {request.EmployeId} n'existe pas.");
        }

        var typeContrat = await _typeContratRepository.GetByIdAsync(request.TypeContratId);
        if (typeContrat == null)
        {
            throw new Exception($"Le type de contrat avec l'ID {request.TypeContratId} n'existe pas.");
        }

        var societe = await _societeRepository.GetByIdAsync(request.SocieteId);
        if (societe == null)
        {
            throw new Exception($"La société avec l'ID {request.SocieteId} n'existe pas.");
        }
        // --- FIN DE LA VÉRIFICATION ---

        var contrat = new Contrat
        {
            Reference = request.Reference,
            DateDebut = request.DateDebut,
            DateFin = request.DateFin,
            Statut = "En validation",
            EmployeId = request.EmployeId,
            TypeContratId = request.TypeContratId,
            SocieteId = request.SocieteId
        };

        var newContrat = await _contratRepository.AddAsync(contrat, cancellationToken);

        // La logique pour le workflow et l'audit log sera ajoutée ici plus tard

        return newContrat.Id;
    }
}
