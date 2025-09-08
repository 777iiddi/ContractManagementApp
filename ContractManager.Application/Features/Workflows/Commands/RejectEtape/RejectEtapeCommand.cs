using MediatR;

namespace ContractManager.Application.Features.Workflows.Commands.RejectEtape;

public class RejectEtapeCommand : IRequest
{
    public int EtapeWorkflowId { get; set; }
    public string Commentaire { get; set; } = string.Empty;
}
