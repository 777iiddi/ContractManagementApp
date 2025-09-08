using MediatR;

namespace ContractManager.Application.Features.Contrats.Commands.ArchiveContrat;

public class ArchiveContratCommand : IRequest<Unit>
{
    public int ContratId { get; set; }
    // Plus tard, nous pourrions ajouter l'ID de l'utilisateur qui effectue l'action pour l'audit.
    // public int UserId { get; set; }
}
