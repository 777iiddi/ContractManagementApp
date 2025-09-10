using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratList;

public class GetTypeContratListQueryHandler : IRequestHandler<GetTypeContratListQuery, List<TypeContratListDto>>
{
    private readonly ITypeContratRepository _typeContratRepository;

    public GetTypeContratListQueryHandler(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<List<TypeContratListDto>> Handle(GetTypeContratListQuery request, CancellationToken cancellationToken)
    {
        var typeContrats = await _typeContratRepository.GetAllWithContractsCountAsync(request.EstActif);

        return typeContrats.Select(tc => new TypeContratListDto
        {
            Id = tc.Id,
            Nom = tc.Nom,
            Description = tc.Description,
            EstActif = tc.EstActif,
            NombreContrats = tc.Contrats?.Count ?? 0
        }).ToList();
    }
}
