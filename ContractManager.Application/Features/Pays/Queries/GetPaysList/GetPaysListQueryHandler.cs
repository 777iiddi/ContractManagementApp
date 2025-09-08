using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Pays.Queries.GetPaysList;

public class GetPaysListQueryHandler : IRequestHandler<GetPaysListQuery, List<PaysDto>>
{
    private readonly IPaysRepository _paysRepository;

    public GetPaysListQueryHandler(IPaysRepository paysRepository)
    {
        _paysRepository = paysRepository;
    }

    public async Task<List<PaysDto>> Handle(GetPaysListQuery request, CancellationToken cancellationToken)
    {
        var allPays = await _paysRepository.GetAllAsync();
        return allPays.Select(p => new PaysDto
        {
            Id = p.Id,
            Nom = p.Nom,
            Code = p.Code
        }).ToList();
    }
}
