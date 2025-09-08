using ContractManager.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Workflows.Commands.RejectEtape;

public class RejectEtapeCommandHandler : IRequestHandler<RejectEtapeCommand>
{
    private readonly IEtapeWorkflowRepository _etapeWorkflowRepository;
    private readonly IContratRepository _contratRepository;

    public RejectEtapeCommandHandler(IEtapeWorkflowRepository etapeWorkflowRepository, IContratRepository contratRepository)
    {
        _etapeWorkflowRepository = etapeWorkflowRepository;
        _contratRepository = contratRepository;
    }

    public async Task<Unit> Handle(RejectEtapeCommand request, CancellationToken cancellationToken)
    {
        var etape = await _etapeWorkflowRepository.GetByIdAsync(request.EtapeWorkflowId);
        if (etape == null || etape.Workflow == null)
        {
            throw new Exception("Étape de workflow introuvable.");
        }

        // 1. Mettre à jour l'étape actuelle
        etape.Statut = "Rejeté";
        etape.Commentaire = request.Commentaire;
        etape.DateAction = DateTime.UtcNow;
        await _etapeWorkflowRepository.UpdateAsync(etape);

        // 2. Mettre à jour le statut global du workflow et du contrat
        etape.Workflow.Statut = "Rejeté";
        var contrat = await _contratRepository.GetByIdAsync(etape.Workflow.ContratId);
        if (contrat != null)
        {
            contrat.Statut = "Rejeté";
            await _contratRepository.UpdateAsync(contrat);
        }

        return Unit.Value;
    }
}
