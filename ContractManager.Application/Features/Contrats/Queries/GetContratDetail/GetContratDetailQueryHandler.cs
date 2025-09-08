using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Queries.GetContratDetail;

public class GetContratDetailQueryHandler : IRequestHandler<GetContratDetailQuery, ContratDetailDto>
{
    private readonly IContratRepository _contratRepository;
    public GetContratDetailQueryHandler(IContratRepository contratRepository) => _contratRepository = contratRepository;

    public async Task<ContratDetailDto> Handle(GetContratDetailQuery request, CancellationToken cancellationToken)
    {
        var contrat = await _contratRepository.GetByIdAsync(request.Id);
        if (contrat == null) throw new Exception("Contrat introuvable.");

        return new ContratDetailDto
        {
            Id = contrat.Id,
            Reference = contrat.Reference,
            Statut = contrat.Statut,
            DateDebut = contrat.DateDebut,
            DateFin = contrat.DateFin,
            NomEmploye = contrat.Employe?.Prenom + " " + contrat.Employe?.Nom,
            TypeDeContrat = contrat.TypeContrat?.Nom,
            NomSociete = contrat.Societe?.Nom,
            Workflow = contrat.Workflow != null ? new WorkflowDto
            {
                Id = contrat.Workflow.Id,
                Statut = contrat.Workflow.Statut,
                Etapes = contrat.Workflow.Etapes.Select(e => new EtapeWorkflowDto
                {
                    Id = e.Id,
                    Ordre = e.Ordre,
                    Statut = e.Statut,
                    RoleValidateur = e.RoleValidateur
                }).ToList()
            } : null
        };
    }
}
