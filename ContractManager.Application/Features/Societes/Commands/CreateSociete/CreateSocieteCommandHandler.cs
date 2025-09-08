using ContractManager.Application.Contracts.Persistence;
using ContractManager.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContractManager.Application.Features.Societes.Commands.CreateSociete;

public class CreateSocieteCommandHandler : IRequestHandler<CreateSocieteCommand, int>
{
    private readonly ISocieteRepository _societeRepository;

    public CreateSocieteCommandHandler(ISocieteRepository societeRepository)
    {
        _societeRepository = societeRepository;
    }

    public async Task<int> Handle(CreateSocieteCommand request, CancellationToken cancellationToken)
    {
        var societe = new Societe { Nom = request.Nom };
        // CORRECTION : On passe maintenant le CancellationToken à la méthode AddAsync.
        var newSociete = await _societeRepository.AddAsync(societe, cancellationToken);
        return newSociete.Id;
    }
}
