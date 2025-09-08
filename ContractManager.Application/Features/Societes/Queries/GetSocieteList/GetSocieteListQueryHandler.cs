using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Societes.Queries.GetSocieteList;

public class GetSocieteListQueryHandler : IRequestHandler<GetSocieteListQuery, List<SocieteDto>>
{
    private readonly ISocieteRepository _societeRepository;

    public GetSocieteListQueryHandler(ISocieteRepository societeRepository)
    {
        _societeRepository = societeRepository;
    }

    public async Task<List<SocieteDto>> Handle(GetSocieteListQuery request, CancellationToken cancellationToken)
    {
        var societes = await _societeRepository.GetAllAsync();
        return societes.Select(s => new SocieteDto
        {
            Id = s.Id,
            Nom = s.Nom
        }).ToList();
    }
}
