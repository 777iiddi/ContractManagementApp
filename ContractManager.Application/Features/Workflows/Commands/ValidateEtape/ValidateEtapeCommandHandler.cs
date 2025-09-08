using ContractManager.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Workflows.Commands.ValidateEtape;

public class ValidateEtapeCommandHandler : IRequestHandler<ValidateEtapeCommand>
{
    private readonly IEtapeWorkflowRepository _etapeWorkflowRepository;
    private readonly IContratRepository _contratRepository;

    public ValidateEtapeCommandHandler(IEtapeWorkflowRepository etapeWorkflowRepository, IContratRepository contratRepository)
    {
        _etapeWorkflowRepository = etapeWorkflowRepository;
        _contratRepository = contratRepository;
    }

    public async Task<Unit> Handle(ValidateEtapeCommand request, CancellationToken cancellationToken)
    {
        var etape = await _etapeWorkflowRepository.GetByIdAsync(request.EtapeWorkflowId);
        if (etape == null || etape.Workflow == null)
        {
            throw new Exception("Étape de workflow introuvable.");
        }

        // 1. Mettre à jour l'étape actuelle
        etape.Statut = "Validé";
        etape.Commentaire = request.Commentaire;
        etape.DateAction = DateTime.UtcNow;
        await _etapeWorkflowRepository.UpdateAsync(etape);

        // 2. Vérifier s'il y a une étape suivante
        var etapeSuivante = etape.Workflow.Etapes.OrderBy(e => e.Ordre).FirstOrDefault(e => e.Ordre > etape.Ordre);

        if (etapeSuivante != null)
        {
            // 3a. Activer l'étape suivante
            etapeSuivante.Statut = "En attente";
            await _etapeWorkflowRepository.UpdateAsync(etapeSuivante);
        }
        else
        {
            // 3b. C'était la dernière étape, le workflow est terminé
            etape.Workflow.Statut = "Validé";
            var contrat = await _contratRepository.GetByIdAsync(etape.Workflow.ContratId);
            if (contrat != null)
            {
                contrat.Statut = "Actif";
                await _contratRepository.UpdateAsync(contrat);
            }
        }

        return Unit.Value;
    }
}