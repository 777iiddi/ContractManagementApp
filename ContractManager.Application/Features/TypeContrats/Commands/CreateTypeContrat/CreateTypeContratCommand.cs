using ContractManager.Application.DTOs;
using MediatR;

namespace ContractManager.Application.Features.TypeContrats.Commands.CreateTypeContrat;

public class CreateTypeContratCommand : IRequest<int>
{
    public string Nom { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int? DureeDefautMois { get; set; }
    public int? PeriodeEssaiDefautJours { get; set; }
    public int? PreavisDefautJours { get; set; }
    public int? ModeleDocumentId { get; set; }
    public List<CreateChampObligatoireDto> ChampsObligatoires { get; set; } = new();
}
