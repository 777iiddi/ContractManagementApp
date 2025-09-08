using MediatR;

namespace ContractManager.Application.Features.Contrats.Commands.ApproveContrat;

public class ApproveContratCommand : IRequest<Unit>
{
    public int ContratId { get; set; }
    public string Commentaire { get; set; } = string.Empty;
}
