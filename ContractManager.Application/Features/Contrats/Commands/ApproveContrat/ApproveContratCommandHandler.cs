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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApproveContratCommandHandler(
        IContratRepository contratRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _contratRepository = contratRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(ApproveContratCommand request, CancellationToken cancellationToken)
    {
        // Récupérer le contrat
        var contrat = await _contratRepository.GetByIdAsync(request.ContratId);
        if (contrat == null)
            throw new Exception("Contrat non trouvé");

        // Simplement mettre à jour le statut du contrat
        contrat.Statut = "Actif";
        await _contratRepository.UpdateAsync(contrat, cancellationToken);

        return Unit.Value;
    }
}
