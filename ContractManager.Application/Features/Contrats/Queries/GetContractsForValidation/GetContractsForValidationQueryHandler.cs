using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims; 
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Queries.GetContractsForValidation;

// CORRECTION : Le type de retour correspond maintenant à ce que la requête attend
public class GetContractsForValidationQueryHandler : IRequestHandler<GetContractsForValidationQuery, List<ContratListDto>>
{
    private readonly IContratRepository _contratRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetContractsForValidationQueryHandler(IContratRepository contratRepository, IHttpContextAccessor httpContextAccessor)
    {
        _contratRepository = contratRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<ContratListDto>> Handle(GetContractsForValidationQuery request, CancellationToken cancellationToken)
    {
        // CORRECTION : Utilisation de FindFirst(...).Value pour une meilleure compatibilité
        var managerIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(managerIdClaim) || !int.TryParse(managerIdClaim, out var managerId))
        {
            return new List<ContratListDto>();
        }

        var contrats = await _contratRepository.GetContractsForValidationByManagerIdAsync(managerId);

        return contrats.Select(c => new ContratListDto
        {
            Id = c.Id,
            Reference = c.Reference,
            NomEmploye = c.Employe != null ? $"{c.Employe.Prenom} {c.Employe.Nom}" : "N/A",
            TypeDeContrat = c.TypeContrat != null ? c.TypeContrat.Nom : "N/A",
            Statut = c.Statut,
            DateDebut = c.DateDebut,
        }).ToList();
    }
}