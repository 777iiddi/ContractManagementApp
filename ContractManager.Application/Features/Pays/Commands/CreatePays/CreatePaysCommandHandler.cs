using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Pays.Commands.CreatePays;

public class CreatePaysCommandHandler : IRequestHandler<CreatePaysCommand, int>
{
    private readonly IPaysRepository _paysRepository;

    public CreatePaysCommandHandler(IPaysRepository paysRepository)
    {
        _paysRepository = paysRepository;
    }

    public async Task<int> Handle(CreatePaysCommand request, CancellationToken cancellationToken)
    {
        // CORRECTION : On utilise le nom complet de l'entité pour éviter le conflit avec le namespace "Pays".
        var pays = new Domain.Entities.Pays { Nom = request.Nom, Code = request.Code };
        var newPays = await _paysRepository.AddAsync(pays, cancellationToken);
        return newPays.Id;
    }
}
