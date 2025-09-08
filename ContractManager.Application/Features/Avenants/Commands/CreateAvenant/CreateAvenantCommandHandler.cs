using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Avenants.Commands.CreateAvenant;

public class CreateAvenantCommandHandler : IRequestHandler<CreateAvenantCommand, int>
{
    private readonly IAvenantRepository _avenantRepository;

    public CreateAvenantCommandHandler(IAvenantRepository avenantRepository)
    {
        _avenantRepository = avenantRepository;
    }

    public async Task<int> Handle(CreateAvenantCommand request, CancellationToken cancellationToken)
    {
        var avenant = new Avenant
        {
            ContratId = request.ContratId,
            Description = request.Description,
            DateModification = request.DateModification
        };
        var newAvenant = await _avenantRepository.AddAsync(avenant, cancellationToken);
        return newAvenant.Id;
    }
}