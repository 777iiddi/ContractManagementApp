using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Queries.GetContratList;

public class GetContratListQueryHandler : IRequestHandler<GetContratListQuery, List<ContratListDto>>
{
    private readonly IContratRepository _contratRepository;

    public GetContratListQueryHandler(IContratRepository contratRepository)
    {
        _contratRepository = contratRepository;
    }

    public async Task<List<ContratListDto>> Handle(GetContratListQuery request, CancellationToken cancellationToken)
    {
        var contrats = await _contratRepository.GetAllContractsAsync();
        return contrats.Select(c => new ContratListDto
        {
            Id = c.Id,
            Reference = c.Reference,
            Statut = c.Statut,
            DateDebut = c.DateDebut,
            NomEmploye = c.Employe != null ? $"{c.Employe.Prenom} {c.Employe.Nom}" : "N/A",
            TypeDeContrat = c.TypeContrat != null ? c.TypeContrat.Nom : "N/A"
        }).ToList();
    }
}
