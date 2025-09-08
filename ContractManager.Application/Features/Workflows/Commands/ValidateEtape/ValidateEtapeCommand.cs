using MediatR;

namespace ContractManager.Application.Features.Workflows.Commands.ValidateEtape;

public class ValidateEtapeCommand : IRequest
{
    public int EtapeWorkflowId { get; set; }
    public string Commentaire { get; set; } = string.Empty;
    // Plus tard, nous ajouterons l'ID de l'utilisateur qui valide.
    // public int UtilisateurId { get; set; }
}
