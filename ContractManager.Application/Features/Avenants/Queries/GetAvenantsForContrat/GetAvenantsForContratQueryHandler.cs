using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Avenants.Queries.GetAvenantsForContrat;

public class GetAvenantsForContratQueryHandler : IRequestHandler<GetAvenantsForContratQuery, List<AvenantDto>>
{
    private readonly IAvenantRepository _avenantRepository;

    public GetAvenantsForContratQueryHandler(IAvenantRepository avenantRepository)
    {
        _avenantRepository = avenantRepository;
    }

    public async Task<List<AvenantDto>> Handle(GetAvenantsForContratQuery request, CancellationToken cancellationToken)
    {
        var avenants = await _avenantRepository.GetAvenantsForContratAsync(request.ContratId);
        return avenants.Select(a => new AvenantDto
        {
            Id = a.Id,
            Description = a.Description,
            DateModification = a.DateModification
        }).ToList();
    }
}
