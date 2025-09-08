using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Commands.RejectContrat;

public class RejectContratCommandHandler : IRequestHandler<RejectContratCommand, Unit>
{
    private readonly IContratRepository _contratRepository;
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RejectContratCommandHandler(
        IContratRepository contratRepository,
        IWorkflowRepository workflowRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _contratRepository = contratRepository;
        _workflowRepository = workflowRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(RejectContratCommand request, CancellationToken cancellationToken)
    {
        var contrat = await _contratRepository.GetByIdAsync(request.ContratId);
        if (contrat == null)
            throw new Exception("Contrat non trouvé");

        // Get current user ID
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = int.TryParse(userIdClaim, out var id) ? id : (int?)null;

        // Create workflow if it doesn't exist
        if (contrat.Workflow == null)
        {
            var workflow = new Workflow
            {
                ContratId = contrat.Id,
                Statut = "Rejeté"
            };
            await _workflowRepository.AddAsync(workflow, cancellationToken);
            contrat.Workflow = workflow;
        }

        // Update contract status
        contrat.Statut = "Rejeté";
        await _contratRepository.UpdateAsync(contrat, cancellationToken);

        return Unit.Value;
    }
}
