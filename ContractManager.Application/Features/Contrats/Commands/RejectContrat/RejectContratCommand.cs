using MediatR;

namespace ContractManager.Application.Features.Contrats.Commands.RejectContrat;

public class RejectContratCommand : IRequest<Unit>
{
    public int ContratId { get; set; }
    public string Commentaire { get; set; } = string.Empty;
}
