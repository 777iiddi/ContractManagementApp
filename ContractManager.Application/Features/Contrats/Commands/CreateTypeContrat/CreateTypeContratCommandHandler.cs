using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.TypeContrats.Commands.CreateTypeContrat;

public class CreateTypeContratCommandHandler : IRequestHandler<CreateTypeContratCommand, int>
{
    private readonly ITypeContratRepository _typeContratRepository;

    public CreateTypeContratCommandHandler(ITypeContratRepository typeContratRepository)
    {
        _typeContratRepository = typeContratRepository;
    }

    public async Task<int> Handle(CreateTypeContratCommand request, CancellationToken cancellationToken)
    {
        var typeContrat = new TypeContrat { Nom = request.Nom };
        var newTypeContrat = await _typeContratRepository.AddAsync(typeContrat, cancellationToken);
        return newTypeContrat.Id;
    }
}
