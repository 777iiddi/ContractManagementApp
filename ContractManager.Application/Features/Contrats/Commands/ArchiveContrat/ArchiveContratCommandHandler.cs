using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Commands.ArchiveContrat;

public class ArchiveContratCommandHandler : IRequestHandler<ArchiveContratCommand, Unit>
{
    private readonly IContratRepository _contratRepository;
    private readonly IAuditLogRepository _auditLogRepository;

    public ArchiveContratCommandHandler(IContratRepository contratRepository, IAuditLogRepository auditLogRepository)
    {
        _contratRepository = contratRepository;
        _auditLogRepository = auditLogRepository;
    }

    public async Task<Unit> Handle(ArchiveContratCommand request, CancellationToken cancellationToken)
    {
        var contrat = await _contratRepository.GetByIdAsync(request.ContratId);

        if (contrat == null)
        {
            throw new Exception($"Contrat avec l'ID {request.ContratId} introuvable.");
        }

        // 1. Changer le statut du contrat
        contrat.Statut = "Archivé";
        await _contratRepository.UpdateAsync(contrat);

        // 2. Créer une entrée dans le journal d'audit pour la traçabilité
        var auditLog = new AuditLog
        {
            Action = "ARCHIVAGE_CONTRAT",
            DateAction = DateTime.UtcNow,
            Details = $"Le contrat ID {contrat.Id} a été archivé.",
            // Plus tard, nous lierons cela à l'utilisateur connecté
            // UtilisateurId = request.UserId 
        };
        await _auditLogRepository.AddAsync(auditLog, cancellationToken);

        return Unit.Value;
    }
}
