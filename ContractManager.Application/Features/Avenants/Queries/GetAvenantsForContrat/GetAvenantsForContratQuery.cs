using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.Avenants.Queries.GetAvenantsForContrat;

public class GetAvenantsForContratQuery : IRequest<List<AvenantDto>>
{
    public int ContratId { get; set; }
}