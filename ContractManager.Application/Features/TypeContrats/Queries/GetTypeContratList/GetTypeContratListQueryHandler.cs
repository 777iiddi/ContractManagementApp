using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetTypeContratList;

public class GetTypeContratListQueryHandler : IRequestHandler<GetTypeContratListQuery, List<TypeContratDto>>
{
    private readonly ITypeContratRepository _typeContratRepository;

    public GetTypeContratListQueryHandler(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<List<TypeContratDto>> Handle(GetTypeContratListQuery request, CancellationToken cancellationToken)
    {
        var typeContrats = await _typeContratRepository.GetAllAsync(); // Assurez-vous que GetAllAsync existe sur votre ITypeContratRepository
        return typeContrats.Select(tc => new TypeContratDto
        {
            Id = tc.Id,
            Nom = tc.Nom
        }).ToList();
    }
}
