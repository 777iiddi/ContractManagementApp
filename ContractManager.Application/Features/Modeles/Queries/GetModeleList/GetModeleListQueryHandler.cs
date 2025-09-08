using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.Features.Modeles.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Modeles.Queries.GetModeleList;

public class GetModeleListQueryHandler : IRequestHandler<GetModeleListQuery, List<ModeleDocumentDto>>
{
    private readonly IModeleDocumentRepository _modeleDocumentRepository;

    public GetModeleListQueryHandler(IModeleDocumentRepository modeleDocumentRepository)
    {
        _modeleDocumentRepository = modeleDocumentRepository;
    }

    public async Task<List<ModeleDocumentDto>> Handle(GetModeleListQuery request, CancellationToken cancellationToken)
    {
        var modeles = await _modeleDocumentRepository.GetAllAsync();
        return modeles.Select(m => new ModeleDocumentDto
        {
            Id = m.Id,
            Nom = m.Nom,
            ContenuTemplate = m.ContenuTemplate
        }).ToList();
    }
}
