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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RejectContratCommandHandler(
        IContratRepository contratRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _contratRepository = contratRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Unit> Handle(RejectContratCommand request, CancellationToken cancellationToken)
    {
        // Récupérer le contrat avec son workflow
        var contrat = await _contratRepository.GetByIdAsync(request.ContratId);
        if (contrat == null)
            throw new Exception("Contrat non trouvé");

        // Simplement mettre à jour le statut du contrat
        contrat.Statut = "Rejeté";
        await _contratRepository.UpdateAsync(contrat, cancellationToken);

        return Unit.Value;
    }
}
