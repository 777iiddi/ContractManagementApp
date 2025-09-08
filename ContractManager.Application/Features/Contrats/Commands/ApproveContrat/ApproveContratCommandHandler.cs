using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Contrats.Commands.ApproveContrat;

public class ApproveContratCommandHandler : IRequestHandler<ApproveContratCommand, Unit>
{
    private readonly IContratRepository _contratRepository;
    private readonly IWorkflowRepository _workflowRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApproveContratCommandHandler(
        IContratRepository contratRepository,
        IWorkflowRepository workflowRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _contratRepository = contratRepository;
        _workflowRepository = workflowRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(ApproveContratCommand request, CancellationToken cancellationToken)
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
                Statut = "Validé"
            };
            await _workflowRepository.AddAsync(workflow, cancellationToken);
            contrat.Workflow = workflow;
        }

        // Update contract status
        contrat.Statut = "Actif";
        await _contratRepository.UpdateAsync(contrat, cancellationToken);

        return Unit.Value;
    }
}
