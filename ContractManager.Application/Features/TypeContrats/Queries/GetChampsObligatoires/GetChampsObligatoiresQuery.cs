using ContractManager.Application.DTOs;
using MediatR;
using System.Collections.Generic;

namespace ContractManager.Application.Features.TypeContrats.Queries.GetChampsObligatoires;

public class GetChampsObligatoiresQuery : IRequest<List<ChampObligatoireDto>>
{
    public int TypeContratId { get; set; }
    public int? PaysId { get; set; }
}
