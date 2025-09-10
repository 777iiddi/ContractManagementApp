using ContractManager.Application.Contracts.Persistence;
using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetChampsObligatoires;

public class GetChampsObligatoiresQueryHandler : IRequestHandler<GetChampsObligatoiresQuery, List<ChampObligatoireDto>>
{
    private readonly ITypeContratRepository _typeContratRepository;

    public GetChampsObligatoiresQueryHandler(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<List<ChampObligatoireDto>> Handle(GetChampsObligatoiresQuery request, CancellationToken cancellationToken)
    {
        var champs = await _typeContratRepository.GetChampsObligatoiresByTypeAndPaysAsync(request.TypeContratId, request.PaysId);

        return champs.Select(co => new ChampObligatoireDto
        {
            Id = co.Id,
            NomChamp = co.NomChamp,
            TypeChamp = co.TypeChamp,
            EstRequis = co.EstRequis,
            ValeurParDefaut = co.ValeurParDefaut,
            ValidationRegex = co.ValidationRegex,
            MessageErreur = co.MessageErreur,
            PaysId = co.PaysId,
            PaysNom = co.Pays?.Nom
        }).ToList();
    }
}
